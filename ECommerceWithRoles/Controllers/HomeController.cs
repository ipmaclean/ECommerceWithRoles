using ECommerceWithRoles.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWithRoles.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            //todo pass in up to 20 random products
            var products = _context.Products.OrderBy(p => Guid.NewGuid());
            if (products.Count() <= 10)
            {
                return View(products);
            }
            else
            {
                return View(products.Take(10));
            }

            
        }

        public async Task<ActionResult> AllProducts(string filter)
        {
            ViewBag.FilterText = "";
            if (!string.IsNullOrEmpty(filter))
            {
                var department = await _context.Departments.FindAsync(Guid.Parse(filter));
                if (department == null)
                {
                    var tag = await _context.Tags.FindAsync(Guid.Parse(filter));
                    ViewBag.FilterText = string.Format("You are filtering by the tag: {0}.", tag.Description);
                    var products = tag.Products.OrderByDescending(p => p.Price);
                    return View(products);
                }
                else
                {
                    ViewBag.FilterText = string.Format("You are filtering by the department: {0}.", department.Name);
                    var products = department.Products.OrderByDescending(p => p.Price);
                    return View(products);
                }
            }
            else
            {
                var products = _context.Products.OrderByDescending(p => p.Price);
                return View(products);
            }
        }

        public ActionResult _SingleProductPartial(Product product)
        {
            return PartialView(product);
        }

        public async Task<ActionResult> SingleProduct(Guid? id)
        {
            if (id != null) {
                var product = await _context.Products.FindAsync(id);

                if (product != null)
                {
                    ViewBag.LastTag = product.Tags == null || product.Tags.Count == 0 ? "null" : product.Tags.Last().Description;
                    return View(product);
                }
            }
            return RedirectToAction("AllProducts");
        }


        public ActionResult _ShoppingBasketPartial()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var basket = _context.ShoppingBaskets.Where(b => b.ApplicationUser.Id.ToString() == userId).FirstOrDefault();
                return PartialView(basket);
            }
            return PartialView();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Basket()
        {
            var userId = User.Identity.GetUserId();
            var basket = _context.ShoppingBaskets.Where(b => b.ApplicationUser.Id.ToString() == userId).FirstOrDefault();
            return View(basket);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Basket(ShoppingBasketItem item)
        {


            var shoppingBasketItem = await _context.ShoppingBasketItems.FindAsync(item.ShoppingBasketItemId);
            shoppingBasketItem.SetQuantity(item.ProductAmount);

            await _context.SaveChangesAsync();


            var basket = await _context.ShoppingBaskets.FindAsync(item.ShoppingBasket.ShoppingBasketId);
            return RedirectToAction("Basket", basket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart([Bind(Include = "ProductId")] Product prod, int? quantity)
        {
            if (User.Identity.IsAuthenticated)
            {
                ShoppingBasketItem shoppingBasketItem;
                var product = await _context.Products.FindAsync(prod.ProductId);
                var userId = User.Identity.GetUserId();
                var basket = _context.ShoppingBaskets.Where(b => b.ApplicationUser.Id.ToString() == userId).FirstOrDefault();

                var foundInBasket = basket.ShoppingBasketItems.Where(m => m.Product == product).FirstOrDefault();
                if (foundInBasket != null)
                {
                    foundInBasket.AddProduct(quantity);
                }
                else
                {
                    shoppingBasketItem = new ShoppingBasketItem()
                    {
                        Product = product,
                        TotalPrice = quantity != null ? (int)quantity * product.Price : product.Price,
                        ProductAmount = quantity != null ? (int)quantity : 1

                    };

                    basket.ShoppingBasketItems.Add(shoppingBasketItem);
                }

                await _context.SaveChangesAsync();

                return PartialView("_ShoppingBasketPartial", basket);
            }

            return PartialView("_ShoppingBasketPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteShoppingBasketItem(ShoppingBasketItem item)
        {
            if (User.Identity.IsAuthenticated)
            {
                var basket = await _context.ShoppingBaskets.FindAsync(item.ShoppingBasket.ShoppingBasketId);
                var itemToDelete = await _context.ShoppingBasketItems.FindAsync(item.ShoppingBasketItemId);
                basket.ShoppingBasketItems.Remove(itemToDelete);
                _context.ShoppingBasketItems.Remove(itemToDelete);

                await _context.SaveChangesAsync();

                return RedirectToAction("Basket", basket);
            }

            return RedirectToAction("Index");


        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            throw new NotImplementedException();
        }

        public ActionResult IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            else
            {
                return Content("false");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWithRoles.Models;
using System.IO;

namespace ECommerceWithRoles.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Product
        public async Task<ActionResult> Index()
        {
            return View(await _context.Products.OrderBy(p => p.Name).ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel();

            var allTagsList = _context.Tags.OrderBy(t => t.Description).ToList();
            productViewModel.AllTagsSelectList = allTagsList.Select(o => new SelectListItem
            {
                Text = o.Description,
                Value = o.TagId.ToString()
            });

            productViewModel.AllTagsString = allTagsList.Select(o => string.Format(o.Description));

            var allDepartmentsList = _context.Departments.OrderBy(d => d.Name).ToList();
            productViewModel.AllDepartments = allDepartmentsList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.DepartmentId.ToString()
            });

            return View(productViewModel);
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            ModelState.Remove("Product.ProductId");
            if (ModelState.IsValid)
            {
                var updatedTags = new HashSet<string>(productViewModel.SelectedTags);
                if (productViewModel.AdditionalTags != null)
                {
                    foreach (var tag in productViewModel.AdditionalTags)
                    {
                        var matchedTag = _context.Tags.Where(t => t.Description.ToLower() == tag.ToLower()).FirstOrDefault();
                        if (matchedTag != null)
                        {
                            updatedTags.Add(matchedTag.TagId.ToString());
                        }
                        else if (tag != "")
                        {
                            var newTag = new Tag()
                            {
                                TagId = Guid.NewGuid(),
                                Description = tag
                            };

                            _context.Tags.Add(newTag);
                            productViewModel.Product.Tags.Add(newTag);
                        }
                    }
                }
                foreach (var tag in _context.Tags)
                {
                    if (!updatedTags.Contains(tag.TagId.ToString()))
                    {
                        productViewModel.Product.Tags.Remove(tag);
                    }
                    else
                    {
                        productViewModel.Product.Tags.Add(tag);
                    }
                }

                productViewModel.Product.Department = await _context.Departments.FindAsync(Guid.Parse(productViewModel.SelectedDepartment));

                if (productViewModel.ImageFile != null)
                {

                    string imageName = Path.GetFileNameWithoutExtension(productViewModel.ImageFile.FileName);
                    string extension = Path.GetExtension(productViewModel.ImageFile.FileName);
                    imageName += extension;
                    productViewModel.Product.Image = imageName;

                    var imagePath = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                    productViewModel.ImageFile.SaveAs(imagePath);
                }

                productViewModel.Product.ProductId = Guid.NewGuid();
                _context.Products.Add(productViewModel.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productViewModel = new ProductViewModel()
            {
                Product = product,
                Tags = product.Tags,
                Department = product.Department
            };

            var allTagsList = _context.Tags.OrderBy(t => t.Description).ToList();
            productViewModel.AllTagsSelectList = allTagsList.Select(o => new SelectListItem
            {
                Text = o.Description,
                Value = o.TagId.ToString()
            });

            productViewModel.AllTagsString = allTagsList.Select(o => string.Format(o.Description));

            var allDepartmentsList = _context.Departments.OrderBy(d => d.Name).ToList();
            productViewModel.AllDepartments = allDepartmentsList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.DepartmentId.ToString()
            });

            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productToUpdate = _context.Products.Include(i => i.Tags).First(i => i.ProductId == productViewModel.Product.ProductId);

                if (TryUpdateModel(productToUpdate, "Product", new string[] { "Name", "Description", "Price" }))
                {
                    var updatedTags = new HashSet<string>(productViewModel.SelectedTags);
                    if (productViewModel.AdditionalTags != null)
                    {
                        foreach (var tag in productViewModel.AdditionalTags)
                        {
                            var matchedTag = _context.Tags.Where(t => t.Description.ToLower() == tag.ToLower()).FirstOrDefault();
                            if (matchedTag != null)
                            {
                                updatedTags.Add(matchedTag.TagId.ToString());
                            }
                            else if (tag != "")
                            {
                                var newTag = new Tag()
                                {
                                    TagId = Guid.NewGuid(),
                                    Description = tag
                                };

                                _context.Tags.Add(newTag);
                                productToUpdate.Tags.Add(newTag);
                            }
                        }
                    }
                    foreach (var tag in _context.Tags)
                    {
                        if (!updatedTags.Contains(tag.TagId.ToString()))
                        {
                            productToUpdate.Tags.Remove(tag);
                        }
                        else
                        {
                            productToUpdate.Tags.Add(tag);
                        }
                    }

                    productToUpdate.Department = await _context.Departments.FindAsync(Guid.Parse(productViewModel.SelectedDepartment));

                    // Save and update image if new, keep original if null
                    if (productViewModel.ImageFile != null)
                    {

                        string imageName = Path.GetFileNameWithoutExtension(productViewModel.ImageFile.FileName);
                        string extension = Path.GetExtension(productViewModel.ImageFile.FileName);
                        imageName += extension;
                        productToUpdate.Image = imageName;

                        var imagePath = Path.Combine(Server.MapPath("~/Content/images/"), imageName);
                        productViewModel.ImageFile.SaveAs(imagePath);
                    }

                    _context.Entry(productToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Product product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

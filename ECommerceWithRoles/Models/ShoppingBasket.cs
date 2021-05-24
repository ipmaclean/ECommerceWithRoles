using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceWithRoles.Models
{
    public class ShoppingBasket
    {
        public ShoppingBasket()
        {
            this.ShoppingBasketItems = new HashSet<ShoppingBasketItem>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShoppingBasketId { get; set; }

        public virtual ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string GetFormattedBasketPrice()
        {
            var totalPrice = 0m;

            foreach (var shoppingBasketItem in ShoppingBasketItems)
            {
                totalPrice += shoppingBasketItem.TotalPrice;
            }

            return totalPrice.ToString("0.00");
        }

        public string GetNumberOfItemsInCart()
        {
            int totalNumber = 0;
            foreach (var item in ShoppingBasketItems)
            {
                totalNumber += item.ProductAmount;
            }

            return totalNumber.ToString();
        }
    }
}
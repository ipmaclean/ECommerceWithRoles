using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceWithRoles.Models
{
    public class ShoppingBasketItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShoppingBasketItemId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int ProductAmount { get; set; }
        [Min(0)]
        public decimal TotalPrice { get; set; }

        public virtual ShoppingBasket ShoppingBasket { get; set; }
        public virtual Product Product { get; set; }

        public string GetFormattedBasketItemPrice()
        {
            var totalPrice = TotalPrice;

            return totalPrice.ToString("0.00");
        }

        public void AddProduct(int? quantity)
        {
            ProductAmount += quantity != null ? ((int)quantity >= 0 ? (int)quantity : 0) : 1;
            TotalPrice += Product.Price * (quantity != null ? ((int)quantity >= 0 ? (int)quantity : 0) : 1);
        }

        public void SetQuantity(int quantity)
        {
            ProductAmount = quantity > 0 ? quantity : 1;
            TotalPrice = Product.Price * (quantity > 0 ? quantity : 1);
        }
    }
}
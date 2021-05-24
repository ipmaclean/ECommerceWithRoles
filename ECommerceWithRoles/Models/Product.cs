using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceWithRoles.Models
{
    public class Product
    {
        public Product()
        {
            this.Tags = new HashSet<Tag>();
            this.ShoppingBasketItems = new HashSet<ShoppingBasketItem>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Min(0)]
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
        public virtual Department Department { get; set; }

        public string GetFormattedPrice()
        {
            return Price.ToString("0.00");
        }
    }
}
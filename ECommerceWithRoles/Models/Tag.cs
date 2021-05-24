using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceWithRoles.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Products = new HashSet<Product>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TagId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
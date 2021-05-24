using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceWithRoles.Models
{
    public class Department
    {
        public Department()
        {
            this.Products = new HashSet<Product>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
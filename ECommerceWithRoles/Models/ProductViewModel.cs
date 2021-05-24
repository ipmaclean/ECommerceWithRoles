using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWithRoles.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public IEnumerable<SelectListItem> AllTagsSelectList { get; set; }
        public IEnumerable<string> AllTagsString { get; set; }

        private List<string> _selectedTags;
        public List<string> SelectedTags
        {
            get
            {
                if (_selectedTags == null && Product != null)
                {
                    _selectedTags = Product.Tags.Select(m => m.TagId.ToString()).ToList();
                }
                return _selectedTags;
            }
            set { _selectedTags = value; }
        }
        public List<string> AdditionalTags { get; set; }
        public Department Department { get; set; }
        public IEnumerable<SelectListItem> AllDepartments { get; set; }

        private string _selectedDepartment;
        public string SelectedDepartment
        {
            get
            {
                if (_selectedDepartment == null && Product != null && Product.Department != null)
                {
                    _selectedDepartment = Product.Department.DepartmentId.ToString();
                }
                return _selectedDepartment;
            }
            set { _selectedDepartment = value; }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class Category
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Nullable<int> SubcategoriesCount { get; set; }
        public string ParentId { get; set; }
        public string ApiCatId { get; set; }
        public string ParentCategoryName { get; set; }
        public Nullable<bool> Status { get; set; }
        public string StatusStr { get; set; }
        public List<Category> Children { get; set; }
    }
}

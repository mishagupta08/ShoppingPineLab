using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineAPPService.Models
{
    public class CategoryResponce
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public object description { get; set; }
        public Images images { get; set; }
        public int subcategoriesCount { get; set; }
        public List<Subcategory> subcategories { get; set; }
    }
    public class Subcategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public object description { get; set; }
        public Images images { get; set; }
        public int subcategoriesCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class Product
    {
        public decimal Id { get; set; }
        public string sku { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Nullable<decimal> minPrice { get; set; }
        public Nullable<decimal> maxPrice { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string numericCode { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string thumbnail { get; set; }
        public string mobile { get; set; }
        public string @base { get; set; }
        public string small { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}

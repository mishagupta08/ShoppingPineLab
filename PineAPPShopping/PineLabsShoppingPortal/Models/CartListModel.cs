using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class CartListModel
    {
        public decimal Id { get; set; }
        public string ProductSku { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<decimal> UserId { get; set; }
        public int Quantity { get; set; }
        public string PriceType { get; set; }
        public Nullable<decimal> ProdPrice { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public string thumbnail { get; set; }
        public string mobile { get; set; }
        public string @base { get; set; }
        public string small { get; set; }
        public string ProductName { get; set; }
    }
}

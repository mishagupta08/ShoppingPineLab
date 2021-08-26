using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class CartDetail
    {
        public decimal Id { get; set; }
        public string ProductSku { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<decimal> UserId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string PriceType { get; set; }
        public Nullable<decimal> ProdPrice { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
    }
}
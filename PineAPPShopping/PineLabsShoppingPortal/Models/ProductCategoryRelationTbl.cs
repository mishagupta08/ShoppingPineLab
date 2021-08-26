using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class ProductCategoryRelationTbl
    {
        public decimal Sno { get; set; }
        public string ProductSku { get; set; }
        public string CategoryId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CategoryName { get; set; }
    }
}
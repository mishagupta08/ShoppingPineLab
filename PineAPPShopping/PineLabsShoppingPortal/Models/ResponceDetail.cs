using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class ResponceDetail
    {
        public bool Status { get; set; }

        public int StatusCode { get; set; }

        public List<Category> CatList { get; set; }

        public List<Product> ResultProdList { get; set; }

        public User UserDetail { get; set; }

        public int CartProductCount { get; set; }

        public string Message { get; set; }

        public Int32 TotalRecordCount { get; set; }

        public ProductDetailContainer ProdDetailContainer { get; set; }

        public List<CartListModel> CartList { get; set; }
        public List<MyOrderReport> OrderReport { get; set; }
        public List<MyOrderDetail> OrderDetail { get; set; }
        public WalletResponse WalletResponse { get; set; }
        public Distributor Distributor { get; set; }
        public string OrderId { get; set; }
        public decimal Balance { get; set; }

    }

}
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class HomeViewModel
    {
        public BillingAddress billingDetail { get; set; }

        public List<Category> CategoryList { get; set; }

        public List<KeyValuePair> CartQuantityCount
        { get; set; }

        public PagewiseProducts PageProductList { get; set; }

        public CartDetailcontainer CartContainer { get; set; }

        public List<Product> ProductList { get; set; }

        public CartDetail cartDeatil { get; set; }

        public ProductDetailContainer ProductDetail { get; set; }

        public Int32 TotalRecordCount { get; set; }

        public Int32 CartProductCount { get; set; }

        public void AssignQty()
        {
            this.CartQuantityCount = new List<KeyValuePair>();
            for (var i = 1; i <= 50; i++)
            {
                this.CartQuantityCount.Add(new KeyValuePair { Key = i, Value = i });
            }
        }

        //public List<MyOrderReport> Report { get; set; }
        public List<MyOrderReport> OrderReport { get; set; }
        public List<MyOrderDetail> OrderDetail { get; set; }
        public string OrderId { get; set; }

    }

    public class PagewiseProducts
    {
        public IPagedList<int> pagerCount { get; set; }
        public List<Product> ProductList { get; set; }
        public string order { get; set; }
        public string sortby { get; set; }
        public string SearchString { get; set; }
        public int CatId { get; set; }
    }

}

public class KeyValuePair
{
    public int Key { get; set; }

    public int Value { get; set; }
}
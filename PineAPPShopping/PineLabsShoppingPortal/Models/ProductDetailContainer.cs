using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PineLabsShoppingPortal.Models
{
    public class ProductDetailContainer
    {
        public WohooProductList ProductDetail { get; set; }

        public Images ProductImages { get; set; }

        public decimal SelectedPrice { get; set; }
        public decimal SelectedQty { get; set; }

        public List<ProductCategoryRelationTbl> ProductCategoryRelation { get; set; }

        public string[] SelectedDenom { get; set; }

        public List<ProductContentTbl> ProductContentList { get; set; }

        public List<ProductDenomination> ProductDenominationList { get; set; }

        public List<ProductDiscount> ProductDiscountList { get; set; }

        public ProductDenomination[] DenominationList
        {
            get
            {
                if (ProductDenominationList != null && ProductDenominationList.Count > 0)
                {
                    return ProductDenominationList.ToArray();
                }
                return null;
            }
        }

        public List<Product> RelatedProductList { get; set; }

        //public List<ProductThemeTbl> ProductThemeTblList { get; set; }

        //public List<RelatedProductList> RelatedProductList { get; set; }
    }

    public partial class ProductDiscount
    {
        public decimal Id { get; set; }
        public string ProductSku { get; set; }
        public string DiscountType { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public string DiscountDesc { get; set; }
        public string CounponCode { get; set; }
        public Nullable<int> CouponPriority { get; set; }
    }

    public partial class ProductDenomination
    {
        public decimal Sno { get; set; }
        public string ProductSku { get; set; }
        public string Denomination { get; set; }
        public bool IsSelected { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }

    public partial class ProductContentTbl
    {
        public decimal Id { get; set; }
        public string ProductSku { get; set; }
        public string ProdContent { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }

    public class Images
    {
        public string sku { get; set; }
        public string thumbnail { get; set; }
        public string mobile { get; set; }
        public string @base { get; set; }
        public string small { get; set; }
        public string image { get; set; }
    }

    public partial class WohooProductList
    {
        public decimal Sno { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string PriceType { get; set; }
        public Nullable<decimal> Min { get; set; }
        public Nullable<decimal> Max { get; set; }
        public string Code { get; set; }
        public string NumericCode { get; set; }
        public string Symbol { get; set; }
        public Nullable<bool> ReloadCardNumber { get; set; }
        public string FormatExpiry { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string Sku { get; set; }
        public string TncLink { get; set; }
        public string Name { get; set; }
        public string VoucherType { get; set; }
        public string CategoryName { get; set; }
    }
}
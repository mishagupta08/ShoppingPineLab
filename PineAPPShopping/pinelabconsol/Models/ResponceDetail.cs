﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pinelabconsol.Models
{
    public class ResponceDetail
    {
        public bool Status { get; set; }

        public int StatusCode { get; set; }

        public TokenResponce AuthenticationToken { get; set; }

        public CategoryResponce CategoryList { get; set; }

        public ProductListContainer ProductList { get; set; }

        //public CarteDetail ProductList { get; set; }

        public ProductDetail ProductDetail { get; set; }

        public OrderRoot CreateOrderResponceDetail { get; set; }

        public User UserDetail { get; set; }

        public int CartProductCount { get; set; }

        public List<GetCategoryListWithFilter_Result> CatList { get; set; }

        public List<GetCartListByUserId_Result> CartList { get; set; }

        //public List<GetProductListWithFilter_Result> ResultProdList { get; set; }

        public string Message { get; set; }

        public string orderContent { get; set; }

        public Int32 TotalRecordCount { get; set; }

        public ProductDetailContainer ProdDetailContainer { get; set; }

        public List<orderReportcs> OrderReport { get; set; }
            public M_ResCardActivated CardActivated { get; set; }
    }
}

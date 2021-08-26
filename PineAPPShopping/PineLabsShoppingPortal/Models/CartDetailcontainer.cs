using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public class CartDetailcontainer
    {
        public List<CartListModel> cartProductList { get; set; }

        public decimal TotalAmountToPay { get; set; }
    }
}
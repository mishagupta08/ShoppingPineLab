using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public partial class BillingAddress
    {
        public decimal Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string lastname { get; set; }
        public string AddLineOne { get; set; }
        public string AddLineTwo { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public decimal OrderId { get; set; }
    }
}
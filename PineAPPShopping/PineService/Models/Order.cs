//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PineService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public decimal Id { get; set; }
        public string status { get; set; }
        public string orderId { get; set; }
        public string refno { get; set; }
        public Nullable<bool> CancelAllowed { get; set; }
        public string CancelallowedWithIn { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string ResponceContent { get; set; }
        public string UserId { get; set; }
    }
}

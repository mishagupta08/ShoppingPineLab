//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PineAPPService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ledger
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> RectimeStamp { get; set; }
        public string TxnID { get; set; }
    }
}

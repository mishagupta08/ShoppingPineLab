//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pinelabconsol.Models
{
    using System;
    
    public partial class GetSMSQue_Result
    {
        public int ID { get; set; }
        public Nullable<int> Formno { get; set; }
        public string SMSBody { get; set; }
        public string SMSType { get; set; }
        public Nullable<decimal> MobileNo { get; set; }
        public string SMSAPI { get; set; }
        public string SMSUSerNAme { get; set; }
        public string SMSPassword { get; set; }
        public string SMSSenderID { get; set; }
        public Nullable<bool> IsSMSSent { get; set; }
        public Nullable<int> CompanyId { get; set; }
    }
}

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
    using System.Collections.Generic;
    
    public partial class JobFinalEmailQue
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public string FromEmailID { get; set; }
        public string ToEmailID { get; set; }
        public string EmailType { get; set; }
        public Nullable<bool> IsMailSent { get; set; }
        public Nullable<System.DateTime> RectimeStamp { get; set; }
        public Nullable<int> JobEmailQueID { get; set; }
    }
}

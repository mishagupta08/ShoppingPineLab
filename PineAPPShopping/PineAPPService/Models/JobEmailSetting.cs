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
    
    public partial class JobEmailSetting
    {
        public int ID { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string SMTPServer { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}

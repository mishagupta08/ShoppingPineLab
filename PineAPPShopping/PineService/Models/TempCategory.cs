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
    
    public partial class TempCategory
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Nullable<int> SubcategoriesCount { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string ParentId { get; set; }
        public string ApiCatId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}

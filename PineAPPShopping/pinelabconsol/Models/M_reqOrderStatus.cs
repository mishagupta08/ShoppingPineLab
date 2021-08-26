using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pinelabconsol.Models
{
   public class M_reqOrderStatus
    {
         public string CompanyId { get; set; }
         public string OrderRefNo { get; set; }
    }
     public class M_reqCardActivate
    {
       // public string CompanyId { get; set; }
         public string OrderID { get; set; }
         public int offset { get; set; }
         public int limit { get; set; }
    }
}

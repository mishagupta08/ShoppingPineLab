using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineAPPService.Models
{
    public class TokenResponce
    {
        public string authorizationCode { get; set; }

        public string token { get; set; }
    }
}

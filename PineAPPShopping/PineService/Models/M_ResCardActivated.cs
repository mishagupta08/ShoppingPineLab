using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineService.Models
{
    public class M_ResCardActivated
    {
        public Product products { get; set; }
        public List<Card> cards { get; set; }
        public Currency currency { get; set; }
        public Delivery delivery { get; set; }
        public int total_cards { get; set; }
        public string deliveryMode { get; set; }
    }
}

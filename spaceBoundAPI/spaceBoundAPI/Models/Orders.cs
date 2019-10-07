using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceBoundAPI.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal SalesTaxAmount { get; set; }
    }
}

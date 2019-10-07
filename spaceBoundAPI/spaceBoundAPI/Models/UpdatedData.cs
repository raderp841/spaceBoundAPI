using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceBoundAPI.Models
{
    public class UpdatedData
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal LocalAmount { get; set; }
        public decimal AmountInUSD { get; set; }
    }
}

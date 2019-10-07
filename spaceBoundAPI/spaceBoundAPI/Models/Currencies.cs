using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceBoundAPI.Models
{
    public class Currencies
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}

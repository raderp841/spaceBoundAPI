using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceBoundAPI.Models
{
    public class Rates
    {
        public string Disclaimer { get; set; }
        public string License { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}

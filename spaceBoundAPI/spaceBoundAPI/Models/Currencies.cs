using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace spaceBoundAPI.Models
{
    public class Currencies
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        [Key]
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime LastUpdatedTime { get; set; }

    }
}

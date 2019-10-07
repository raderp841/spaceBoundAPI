using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spaceBoundAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;

namespace spaceBoundAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly spaceBoundAPIContext _context;

        public CurrenciesController(spaceBoundAPIContext context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currencies>>> GetCurrency()
        {
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        [EnableCors]
        public async Task<ActionResult<Currencies>> GetCurrency(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(int id, Currencies currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Currencies
        [HttpPost]
        [EnableCors]
        public async Task<IEnumerable<Currencies>> PostCurrency()
        {
            List<Currencies> currencies = await GetCurrenciesAsync();

            foreach (Currencies c in currencies)
            {
                if (_context.Currencies.Any( x => x.CurrencyCode == c.CurrencyCode))
                {
                }
                else
                {
                    _context.Currencies.Add(c);
                }
            }

            int status = await _context.SaveChangesAsync();
            return await GetExchangeRateAsync();
        }

        // DELETE: api/Currencies/5
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Currencies>> DeleteCurrency(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return currency;
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.Id == id);
        }

        private async Task<List<Currencies>> GetCurrenciesAsync()
        {
            string uriCurrencies = "https://openexchangerates.org/api/currencies.json";
            Dictionary<string, string> result = null;
            List<Currencies> currencies = new List<Currencies>();
            using (HttpClient http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(uriCurrencies);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                }
                foreach (var kvp in result)
                {
                    Currencies c = new Currencies();
                    c.CurrencyCode = kvp.Key;
                    c.CurrencyName = kvp.Value;
                    c.LastUpdatedTime = DateTime.Now;
                    currencies.Add(c);
                }
                return currencies;
            }
        } 
        
        public async Task<List<Currencies>> GetExchangeRateAsync()
        {
            string uri = "https://openexchangerates.org/api/latest.json?app_id=789cf4ba3ca9445e95472f606c42b1ee";
            Rates rates = new Rates();
            List<Currencies> currencies = new List<Currencies>();
            using (HttpClient http = new HttpClient())
            { 
                HttpResponseMessage response = await http.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    rates = await response.Content.ReadAsAsync<Rates>();
                }
                foreach( var rate in rates.rates)
                {
                    //Problem next line
                    Currencies c = await _context.Currencies.Include(cur => cur.CurrencyCode == rate.Key).FirstOrDefaultAsync();
                    c.ExchangeRate = rate.Value;
                }
                await _context.SaveChangesAsync();
                currencies = await _context.Currencies.ToListAsync();
                return currencies;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace spaceBoundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // GET: api/Data
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Data/5
        [HttpGet("{id}", Name = "Get")]
        public Dictionary<string, string> Get(int id)
        {
            string uriLatest = "https://openexchangerates.org/api/currencies.json?app_id=789cf4ba3ca9445e95472f606c42b1ee";

            Task<Dictionary<string, string> > data = GetOriginalDataAsync();
            data.Wait();
            return data.Result;
            
        }

        //static async Task<Product> GetProductAsync(string path)
        //{
        //    Product product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<Product>();
        //    }
        //    return product;
        //}

        // POST: api/Data
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task<Dictionary<string, string>> GetOriginalDataAsync()
        {
            string uriCurrencies = "https://openexchangerates.org/api/currencies.json";
            Dictionary<string, string> result = null;
            using(HttpClient http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(uriCurrencies);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                }
                return result;
            }
        }
    }
}

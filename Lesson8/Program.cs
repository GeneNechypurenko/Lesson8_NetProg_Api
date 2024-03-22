using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lesson8
{
    public class Exchange
    {
        public string Ccy { get; set; }
        public string BaseCcy { get; set; } = "UAH";
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
        public override string ToString() => $"Курс {Ccy}/{BaseCcy}: Покупка - {Buy}, Продажа - {Sale}";
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string apiUrl = "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JArray exchangesJson = JArray.Parse(jsonResponse);

                List<Exchange> exchanges = new List<Exchange>();

                foreach (var exchangeJson in exchangesJson)
                {
                    Exchange exchange = JsonConvert.DeserializeObject<Exchange>(exchangeJson.ToString());
                    exchanges.Add(exchange);
                }

                foreach (var exchange in exchanges)
                {
                    Console.WriteLine(exchange.ToString());
                }
            }
        }
    }
}

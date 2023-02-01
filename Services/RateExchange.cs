using ScorersReporter.Models;
using Newtonsoft.Json;

namespace ScorersReporter.Services
{
    public class RateExchange
    {
        public IEnumerable<decimal> Rate()
        {
            var httpClient = new HttpClient();
            var baseAddress = "https://api.nbp.pl/api/exchangerates/rates/";
            httpClient.BaseAddress = new Uri(baseAddress);

            var response = httpClient.GetAsync("A/EUR/?format=json").Result;
            var contentJson = response.Content.ReadAsStringAsync().Result;
            var series = JsonConvert.DeserializeObject<ExchangeRatesSeries>(contentJson);

            var rateValue = series.Rates.Select(x => x.Mid);
            
            return rateValue;
        }
    }
}

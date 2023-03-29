using ScorersReporter.Models;
using Newtonsoft.Json;

namespace ScorersReporter.Services
{
    public class NBPApiService
    {
        public async Task<decimal> GetRate()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseAddress = "https://api.nbp.pl/api/exchangerates/rates/"; //appsettings
                httpClient.BaseAddress = new Uri(baseAddress);

                var response = await httpClient.GetAsync("A/EUR/?format=json"); //tez

                if (response != null)
                {
                    var contentJson = await response.Content.ReadAsStringAsync();
                    var series = JsonConvert.DeserializeObject<ExchangeRatesSeries>(contentJson);
                    var rateValue = series.Rates.Select(x => x.Mid).FirstOrDefault();
                    return rateValue;
                }


            }

            catch(HttpRequestException ex)
            {
               
            }

            return 0;

        }
    }
}

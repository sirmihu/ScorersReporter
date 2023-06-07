using NbpApiServices.Models;
using Newtonsoft.Json;
using NbpApiServices.Exceptions;
using NbpApiServices.Utiles;

namespace NbpApiServices
{
    public class NbpApiService : INbpApiService
    {
        public async Task<decimal> GetRate()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(NbpApiUrl.BaseAddress);

                var response = await httpClient.GetAsync(NbpApiUrl.EurExchangeRateAddress);

                var contentJson = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<ExchangeRatesSeries>(contentJson);
                var rateValue = series.Rates.Select(x => x.Mid).FirstOrDefault();

                return rateValue;
            }

            catch (HttpRequestException ex)
            {
                throw new NbpApiException($"There was some error in api communication. Message:{ex.Message}");
            }


        }
    }
}

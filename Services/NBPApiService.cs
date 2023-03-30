using ScorersReporter.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using ScorersReporter.Exceptions;

namespace ScorersReporter.Services
{
    public class NBPApiService
    {
        private readonly AppSettings _appSettings;
        public NBPApiService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<decimal> GetRate()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseAddress = _appSettings.SingleCurrency;
                httpClient.BaseAddress = new Uri(baseAddress);

                var response = await httpClient.GetAsync(_appSettings.EurExchangeRate);

                var contentJson = await response.Content.ReadAsStringAsync();
                var series = JsonConvert.DeserializeObject<ExchangeRatesSeries>(contentJson);
                var rateValue = series.Rates.Select(x => x.Mid).FirstOrDefault();

                return rateValue;
            }

            catch(HttpRequestException ex)
            {
                throw new NbpApiException($"There was some error in api communication. Message:{ex.Message}");
            }

        }
    }
}

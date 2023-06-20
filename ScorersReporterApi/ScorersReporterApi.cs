using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ScorersReporterApi.Exceptions;
using ScorersReporterApi.Models;
using ScorersReporterApi.Reponses;
using ScorersReporterApi.Utils;
using Microsoft.Extensions.Options;

namespace ScorersReporterApi
{
	public class ScorersReporterApi : IScorersRepoterApi
	{
        private readonly IScorersReporterHttpClient _httpClient;
        private readonly AppSettings _appSettings;

		public ScorersReporterApi(IScorersReporterHttpClient httpClient, IOptions<AppSettings> options)
		{
            _httpClient = httpClient;
            _appSettings = options.Value;
		}

        public async Task<IEnumerable<ScorerByLeagueResponse>> GetScorersByLeagueReport()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScorerResponse>> GetScorersReport()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(_appSettings.GetScoresReport);

                httpResponse.EnsureSuccessStatusCode();

                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorers = Deserialize<IEnumerable<ScorerResponse>>(response);

                return scorers;
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        public async Task<IEnumerable<CanadianScorerResponse>> GetTop5CanadiansClassificationScorersReport()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(_appSettings.GetTop5CanadianClassificationScorers);

                httpResponse.EnsureSuccessStatusCode();

                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorers = Deserialize<IEnumerable<CanadianScorerResponse>>(response);

                return scorers;
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        public async Task<TopScorerResponse> GetTopScorerReport()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(_appSettings.GetTopScorer);

                httpResponse.EnsureSuccessStatusCode();

                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorer = Deserialize<TopScorerResponse>(response);

                return scorer;
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        public async Task SaveFileToDatabase(ScorerFile scorerFile)
        {
            try
            {
                var httpResponse = await _httpClient.PostFileAsync(scorerFile.File, _appSettings.SaveFileToDatabase);
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        public async Task SaveScorersReportOnDesktop()
        {
            try
            {
                var httpResponse = await _httpClient.PostAsync(_appSettings.SaveReportOnDesktop);
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        public async Task DownloadScorersReport()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(_appSettings.DownloadScorersReport);
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }

        private T Deserialize<T>(string httpResponse)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(httpResponse);
            }
            catch (Exception ex)
            {
                throw new ScorersReporterApiException(ex.Message);
            }
        }
    }
}


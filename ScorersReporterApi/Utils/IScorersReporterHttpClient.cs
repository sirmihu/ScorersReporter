using Microsoft.AspNetCore.Http;

namespace ScorersReporterApi.Utils
{
    public interface IScorersReporterHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url);
        Task<HttpResponseMessage> PostFileAsync(IFormFile file, string url);
    }
}
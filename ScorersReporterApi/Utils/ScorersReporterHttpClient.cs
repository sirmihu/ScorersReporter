using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using ScorersReporterApi.Models;

namespace ScorersReporterApi.Utils
{
    public class ScorersReporterHttpClient : IScorersReporterHttpClient
    {
        private readonly HttpClient _httpClient;

        public ScorersReporterHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var path = new Uri($"{ScorersReporterApiUrl.ScorersReporterApiBaseUrl}/{url}");
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            return await SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostFileAsync(IFormFile file, string url)
        {
            var path = new Uri($"{ScorersReporterApiUrl.ScorersReporterApiBaseUrl}/{url}");
            var fileByteArrayContent = await GetFileByteArrayContent(file);
            var formData = GetPreparedFormData(fileByteArrayContent, file.FileName);

            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = formData;

            return await SendAsync(request);

        }

        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            var path = new Uri($"{ScorersReporterApiUrl.ScorersReporterApiBaseUrl}/{url}");
            var request = new HttpRequestMessage(HttpMethod.Post, path);

            return await SendAsync(request);

        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
            => await _httpClient.SendAsync(requestMessage);

        private async Task<ByteArrayContent> GetFileByteArrayContent(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            return new ByteArrayContent(memoryStream.ToArray());
        }

        private MultipartFormDataContent GetPreparedFormData(ByteArrayContent fileByteArrayContent, string fileName)
        {
            var formData = new MultipartFormDataContent();
            fileByteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            formData.Add(fileByteArrayContent, "File", fileName);

            return formData;
        }
    }
}


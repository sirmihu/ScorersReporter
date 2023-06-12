using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScorersReporterWebApp.Models;

namespace ScorersReporterWebApp.Controllers
{
    public class ScorersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await GetScorers());
        }
        
        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7114");               
                var fileByteArrayContent = await GetFileByteArrayContent(file);
                var formData = GetPreparedFormData(fileByteArrayContent, file.FileName);

                await client.PostAsync("Scorer/SaveFileToDatabase", formData);
            }

            return View(await GetScorers());
        }

        private async Task<IEnumerable<ScorerViewModel>> GetScorers()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7114");

            var httpResponse = await client.GetAsync("Scorer/GetScorersReport");

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorers = JsonConvert.DeserializeObject<IEnumerable<ScorerViewModel>>(response);

                return scorers;
            }
            else throw new System.Exception();
        }

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
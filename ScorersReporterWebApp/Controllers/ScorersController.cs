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
            return View(await GetScorersDashboard());
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

            return View(await GetScorersDashboard());
        }

        [HttpPost]
        public async Task SaveReportOnDesktop()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7114");

                await client.PostAsync("Scorer/SaveScorersReportOnDesktop", null);
            }
        }

        public async Task<PartialViewResult> GetScorers(string league)
        {
            var scorers = await GetScorers();

            if (!string.IsNullOrEmpty(league))
                scorers = scorers.Where(scorer => scorer.League.ToLower().Contains(league.ToLower()));

            return PartialView("_ScorersTablePartial", scorers);
        }

        private async Task<ScorersDashboardViewModel> GetScorersDashboard()
        {
            var scorers = await GetScorers();
            var top5CanadianScorers = await GetTop5CanadianScorers();
            var topScorer = await GetTopScorer();

            return new ScorersDashboardViewModel
            {
                Scorers = scorers,
                Top5CanadianScorers = top5CanadianScorers,
                TopScorer = topScorer
            };

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

        private async Task<IEnumerable<CanadianScorerViewModel>> GetTop5CanadianScorers()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7114");

            var httpResponse = await client.GetAsync("Scorer/GetTop5CanadiansClassificationScorers");

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorers = JsonConvert.DeserializeObject<IEnumerable<CanadianScorerViewModel>>(response);

                return scorers;
            }
            else throw new System.Exception();
        }

        private async Task<TopScorerViewModel> GetTopScorer()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7114");

            var httpResponse = await client.GetAsync("Scorer/GetTopScorer");

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                var scorer = JsonConvert.DeserializeObject<TopScorerViewModel>(response);

                return scorer;
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
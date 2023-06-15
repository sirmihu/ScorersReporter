using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScorersReporterApi;
using ScorersReporterApi.Models;
using ScorersReporterWebApp.Models;

namespace ScorersReporterWebApp.Controllers
{
    public class ScorersController : Controller
    {
        private readonly IScorersRepoterApi _scorersReporterApi;
        private readonly IMapper _mapper;

        public ScorersController(IScorersRepoterApi scorersRepoterApi, IMapper mapper)
        {
            _scorersReporterApi = scorersRepoterApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetScorersDashboard());
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            await _scorersReporterApi.SaveFileToDatabase(new ScorerFile { File = file });

            return View(await GetScorersDashboard());
        }

        [HttpPost]
        public async Task SaveReportOnDesktop()
            => await _scorersReporterApi.SaveScorersReportOnDesktop();

        public async Task<PartialViewResult> GetScorers(string league)
        {
            var scorersResponse = await _scorersReporterApi.GetScorersReport();
            var scorers = _mapper.Map<IEnumerable<ScorerViewModel>>(scorersResponse);

            if (!string.IsNullOrEmpty(league))
                scorers = scorers.Where(scorer => scorer.League.ToLower().Contains(league.ToLower()));

            return PartialView("_ScorersTablePartial", scorers);
        }

        private async Task<ScorersDashboardViewModel> GetScorersDashboard()
        {
            var scorers = await _scorersReporterApi.GetScorersReport();
            var top5CanadianScorers = await _scorersReporterApi.GetTop5CanadiansClassificationScorersReport();
            var topScorer = await _scorersReporterApi.GetTopScorerReport();

            return new ScorersDashboardViewModel
            {
                Scorers = _mapper.Map<IEnumerable<ScorerViewModel>>(scorers),
                Top5CanadianScorers = _mapper.Map<IEnumerable<CanadianScorerViewModel>>(top5CanadianScorers),
                TopScorer = _mapper.Map<TopScorerViewModel>(topScorer)
            };

        }
    }
}
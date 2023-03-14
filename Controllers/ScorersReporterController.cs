using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Models;
using ScorersReporter.Services;

namespace ScorersReporter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScorerController : Controller
    {
        private readonly IScorersReporterService _scorersReporterService;

        public ScorerController(IScorersReporterService scorersReporterService)
        {
            _scorersReporterService = scorersReporterService;
        }
        

        [HttpPost("SaveReportToDatabase")]
        public ActionResult SaveReportToDatabase([FromForm] IFormFileCollection file)
        {
            var records = _scorersReporterService.SaveToDatabase<Scorer>(file[0].OpenReadStream());
          
            return Ok(records);
        }

        [HttpGet("ReportFromDatabase")]
        public async Task<ActionResult<List<ScorerViewModel>>> ReportFromDatabase()
        {
            var records = await _scorersReporterService.DatabaseReport();

            return Ok(records);
        }

        [HttpGet("ReportByLeague")]
        public ActionResult<List<ScorerViewModel>> ReportByLeague([FromQuery] string league)
        {
            var records = _scorersReporterService.LeagueReport(league);       

            return Ok(records);
        }

        

        [HttpGet("TopScorer")]
        public ActionResult<List<ScorerViewModel>> TSReport()
        {
            var records = _scorersReporterService.TopScorerReport();

            return Ok(records);
        }

        [HttpGet("Top5CanadiansClassificationScorers")]
        public ActionResult<List<ScorerViewModel>> Top5CanadiansClassificationScorers()
        {
            var records = _scorersReporterService.Top5CCS();

            return Ok(records);
        }

        [HttpGet("DownloadScorersReport")]
        public void DownloadScorersReport()
        {
            _scorersReporterService.DownloadCsvFile();
        }

    }
}




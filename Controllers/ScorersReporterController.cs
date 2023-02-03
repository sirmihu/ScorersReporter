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
        public ActionResult<IEnumerable<dynamic>> ReportFromDatabase()
        {
            var records = _scorersReporterService.DbReport();

            return Ok(records);
        }

        [HttpGet("ReportByLeague")]
        public ActionResult<IEnumerable<dynamic>> ReportByLeague()
        {
            var records = _scorersReporterService.LeagueReport();

            return Ok(records);
        }

        [HttpGet("TopScorer")]
        public ActionResult<IEnumerable<dynamic>> TopScorer()
        {
            var records = _scorersReporterService.TopScorer();

            return Ok(records);
        }

        [HttpGet("Top5CanadiansClassificationScorers")]
        public ActionResult<IEnumerable<dynamic>> Top5CCS()
        {
            var records = _scorersReporterService.Top5CCS();

            return Ok(records);
        }

        [HttpGet("DownloadScorersReport")]
        public FileContentResult DownloadScorersReport()
        {
            var result = _scorersReporterService.DownloadCsvFile();

            return result;
        }

    }
}




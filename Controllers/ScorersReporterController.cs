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


        [HttpPost("SaveFileToDatabase")]
        public ActionResult SaveFileToDatabase([FromForm] ScorerFile scorerFile)
        {
            _scorersReporterService.SaveToDatabase<Scorer>(scorerFile.formFile.OpenReadStream());
            
            return Ok();
        }

        [HttpGet("GetScorersReport")]
        public async Task<ActionResult<List<ScorerViewModel>>> GetScorersReport()
        {
            var records = await _scorersReporterService.DatabaseReport();

            return Ok(records);
        }

        [HttpGet("GetScorersByLeague")]
        public ActionResult<List<ScorerByLeagueViewModel>> GetScorersByLeague([FromQuery] string league)
        {
            var records = _scorersReporterService.LeagueReport(league);

            return Ok(records);
        }



        [HttpGet("GetTopScorer")]
        public ActionResult<TopScorerViewModel> GetTopScorer()
        {
            var records = _scorersReporterService.TopScorerReport();

            return Ok(records);
        }

        [HttpGet("GetTop5CanadiansClassificationScorers")]
        public ActionResult<List<CanadianScorerViewModel>> GetTop5CanadiansClassificationScorers()
        {
            var records = _scorersReporterService.Top5CCS();

            return Ok(records);
        }

        [HttpGet("SaveScorersReportOnDesktop")]
        public void SaveScorersReportOnDesktop()
        {
            _scorersReporterService.DownloadCsvFile();
        }

    }
}




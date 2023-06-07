using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Application;
using ScorersReporter.Models;

namespace ScorersReporter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScorerController : Controller
    {
        private readonly IScorersReporterApplication _scorersReporterApplication;

        public ScorerController(IScorersReporterApplication scorersReporterApplication)
        {
            _scorersReporterApplication = scorersReporterApplication;
        }


        [HttpPost("SaveFileToDatabase")]
        public ActionResult SaveFileToDatabase([FromForm] ScorerFile scorerFile)
        {
            _scorersReporterApplication.SaveScorersToDatabase(scorerFile.formFile.OpenReadStream());
            
            return Ok();
        }

        [HttpGet("GetScorersReport")]
        public async Task<ActionResult<List<ScorerViewModel>>> GetScorersReport()
        {
            var records = await _scorersReporterApplication.GetReportFromDatabase();

            return Ok(records);
        }

        [HttpGet("GetScorersByLeague")]
        public ActionResult<List<ScorerByLeagueViewModel>> GetScorersByLeagueReport([FromQuery] string league)
        {
            var records = _scorersReporterApplication.GetScorersByLeagueReport(league);

            return Ok(records);
        }



        [HttpGet("GetTopScorer")]
        public ActionResult<TopScorerViewModel> GetTopScorerReport()
        {
            var records = _scorersReporterApplication.GetTopScorerReport();

            return Ok(records);
        }

        [HttpGet("GetTop5CanadiansClassificationScorers")]
        public ActionResult<List<CanadianScorerViewModel>> GetTop5CanadiansClassificationScorersReport()
        {
            var records = _scorersReporterApplication.GetTop5CanadianClassificationScorersReport();

            return Ok(records);
        }

        [HttpGet("SaveScorersReportOnDesktop")]
        public void SaveScorersReportOnDesktop()
        {
            _scorersReporterApplication.SaveScorersReportOnDesktop();
        }

    }
}



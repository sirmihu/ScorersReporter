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
        public async Task<IActionResult> SaveReportToDatabase([FromForm] IFormFileCollection file)
        {
            var records = _scorersReporterService.SaveToDatabase<Scorer>(file[0].OpenReadStream());

            return Ok(records);
        }

        [HttpGet("GetReportFromDatabase")]
        public ActionResult<IEnumerable<dynamic>> GetReportFromDatabase()
        {
            var records = _scorersReporterService.DbReport();

            return Ok(records);
        }

        [HttpGet("GetReportByLeague")]
        public ActionResult<IEnumerable<dynamic>> GetReportByLeague()
        {
            var records = _scorersReporterService.LeagueReport();

            return Ok(records);
        }

        //

    }
}




using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Models;
using ScorersReporter.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;

namespace ScorersReporter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScorerController : Controller
    {
        private readonly ICSVService _csvService;


        public ScorerController(ICSVService csvService)
        {
            _csvService = csvService;
        }

        [HttpPost("GetScorersReport")]
        public async Task<IActionResult> GetScorersReportCSV([FromForm] IFormFileCollection file)
        { 
            var scorers = _csvService.ReadCSV<ScorersReport>(file[0].OpenReadStream());
            return Ok(scorers);
        }
    }
}




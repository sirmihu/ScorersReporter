using ScorersReporter.Models;
using Microsoft.Extensions.Options;
using ScorersReporter.Services;
using FileServices;

namespace ScorersReporter.Application
{
    public class ScorersReporterApplication : IScorersReporterApplication
    {
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;
        private readonly IScorersDbService _dbService;
        private readonly IScorerMapToScorerDetails _scorerDetails;
        private readonly IReportFromDatabase _reportFromDatabase;
        public ScorersReporterApplication(
            IOptions<AppSettings> options,
            IFileService fileService,
            IScorersDbService dbService,  
            IReportFromDatabase reportFromDatabase, 
            IScorerMapToScorerDetails scorerDetails)
        {
            _appSettings = options.Value;
            _fileService = fileService;
            _dbService = dbService;
            _reportFromDatabase = reportFromDatabase;
            _scorerDetails = scorerDetails;
        }
        
        public async Task<List<ScorerViewModel>> GetReportFromDatabase()
        {
            var dbReport = await _reportFromDatabase.GetDbReport();

            return dbReport;
        }

        public List<ScorerByLeagueViewModel> GetScorersByLeagueReport(string league)
        {
            var scorerDetails = _scorerDetails.ScorerDetails().AsQueryable();


            if (!string.IsNullOrEmpty(league))
            {
                scorerDetails = scorerDetails.Where(s => s.League == league);
            }

            var scorerByLeague = scorerDetails.GroupBy(x => x.FullName)
                .Select(g => new ScorerByLeagueViewModel
                {
                    League = g.Select(s => s.League).FirstOrDefault(),
                    Club = g.Select(s => s.Club).FirstOrDefault(),
                    FullName = g.Key,
                    Age = g.Select(s => s.Age).FirstOrDefault(),
                    Country = g.Select(s => s.Country).FirstOrDefault(),
                    TotalGoals = g.Sum(s => s.Goals),
                    TotalAssists = g.Sum(s => s.Assists)
                })
                .OrderBy(g => g.Club)
                .ToList();

            return scorerByLeague;
        }

        public List<CanadianScorerViewModel> GetTop5CanadianClassificationScorersReport()
        {
            var scorerDetails = _scorerDetails.ScorerDetails();

            var top5CanadiansClassificationScorers = scorerDetails.GroupBy(x => x.FullName)
                .Select(g => new CanadianScorerViewModel
                {
                    FullName = g.Key,
                    Points = g.Sum(s => s.Points)
                })
                .OrderByDescending(g => g.Points)
                .Take(5)
                .ToList();

            return top5CanadiansClassificationScorers;
        }

        public List<TopScorerViewModel> GetTopScorerReport()
        {
            var scorerDetails = _scorerDetails.ScorerDetails();

            var topScorer = scorerDetails.GroupBy(x => x.FullName)
                .Select(g => new TopScorerViewModel
                {
                    FullName = g.Key,
                    TotalGoals = g.Sum(s => s.Goals)
                })
                .OrderByDescending(g => g.TotalGoals)
                .Take(1)
                .ToList();

            return topScorer;
        }

        public void SaveScorersToDatabase(Stream file)
        {
            var scorers = _fileService.ReadCSV<Scorer, ScorersReporterClassMap>(file);
            _dbService.SaveToDatabase(scorers);
        }

        public async Task SaveScorersReportOnDesktop()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = desktopPath + _appSettings.FileName;

            var records = await _reportFromDatabase.GetDbReport();

            _fileService.WriteCSV(records, filePath);
        }

    }
}

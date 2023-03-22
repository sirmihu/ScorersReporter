using ScorersReporter.Models;
using ScorersReporter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ScorersReporter.FileServices;

namespace ScorersReporter.Services
{
    public class ScorersReporterService : IScorersReporterService
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly FileReader _fileReader;
        private readonly FileWriter _fileWriter;
        private readonly ScorerMapToScorerDetails _scorerDetails;
        private readonly IFileServices _fileServices;
        private readonly IReportFromDatabase _reportFromDatabase;
        public ScorersReporterService(
            ScorersReportDbContext dbContext, 
            FileReader fileReader, 
            IReportFromDatabase reportFromDatabase, 
            ScorerMapToScorerDetails scorerDetails, 
            IFileServices fileServices)
        {
            _dbContext = dbContext;
            _fileReader = fileReader;
            _reportFromDatabase = reportFromDatabase;
            _scorerDetails = scorerDetails;
            _fileServices = fileServices;
        }

        public IEnumerable<T> SaveToDatabase<T>(Stream file)
        {
            var records = _fileReader.ReadCsv<T>(file);

            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Scorers.AddRange((IEnumerable<Scorer>)records);
                _dbContext.SaveChanges();
            }

            return records;
        }
    
        public async Task<List<ScorerViewModel>> DatabaseReport()
        {
            var dbReport = await _reportFromDatabase.DbReport();

            return dbReport;
        }

        public List<ScorerByLeagueViewModel> LeagueReport(string league)
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

        public List<TopScorerViewModel> TopScorerReport()
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

        public List<CanadianScorerViewModel> Top5CCS()
        {
            var scorerDetails = _scorerDetails.ScorerDetails();

            var topCanadiansClassificationScorers = scorerDetails.GroupBy(x => x.FullName)
                .Select(g => new CanadianScorerViewModel
                {
                    FullName = g.Key,
                    Points = g.Sum(s => s.Points)
                })
                .OrderByDescending(g => g.Points)
                .Take(5)
                .ToList();

            return topCanadiansClassificationScorers;
        }

        public async Task DownloadCsvFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = desktopPath + ""; //appsettingsy

            var records = await _reportFromDatabase.DbReport();

            _fileServices.WriteCSV(records, filePath);
        }

    }
}

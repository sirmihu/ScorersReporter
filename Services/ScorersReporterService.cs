using ScorersReporter.Models;
using ScorersReporter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ScorersReporter.Services
{
    public class ScorersReporterService : IScorersReporterService
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly FileReader _fileReader;
        private readonly ReportFromDatabase _reportFromDatabase;
        private readonly ScorerDetailsDtos _detailsDtos;
        private readonly FileWriter _fileWriter;
        public ScorersReporterService(ScorersReportDbContext dbContext, FileReader fileReader, ReportFromDatabase reportFromDatabase, ScorerDetailsDtos detailsDtos, FileWriter fileWriter)
        {
            _dbContext = dbContext;
            _fileReader = fileReader;
            _reportFromDatabase = reportFromDatabase;
            _detailsDtos = detailsDtos;
            _fileWriter = fileWriter;
        }

        public IEnumerable<T> SaveToDatabase<T>(Stream file)
        {
            var records = _fileReader.ReadCSV<T>(file);

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

        public List<ScorerViewModel> LeagueReport(string league)
        {
            var scorersDtos = _detailsDtos.ScorerDto().AsQueryable();

            if (!string.IsNullOrEmpty(league))
            {
                scorersDtos = scorersDtos.Where(s => s.League == league);
            }

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new ScorerViewModel
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
            
            return records;
        }

        public List<TopScorerViewModel> TopScorerReport()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new TopScorerViewModel
                {
                    FullName = g.Key,
                    TotalGoals = g.Sum(s => s.Goals)
                })
                .OrderByDescending(g => g.TotalGoals)
                .Take(1)
                .ToList();

            return records;
        }

        public List<CanadianScorerViewModel> Top5CCS()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new CanadianScorerViewModel
                {
                    FullName = g.Key,
                    Points = g.Sum(s => s.Points)
                })
                .OrderByDescending(g => g.Points)
                .Take(5)
                .ToList();

            return records;
        }

        public void DownloadCsvFile()
        {
            _fileWriter.WriteCSV();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);
        public Task<List<DbScorer>> DatabaseReport();
        public List<LeagueScorer> LeagueReport(string league);
        public List<TopScorer> TopScorerReport();
        public List<CCScorer> Top5CCS();
        public void DownloadCsvFile();
    }
}
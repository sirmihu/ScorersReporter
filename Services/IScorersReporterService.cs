using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);
        public Task<List<ScorerViewModel11>> DatabaseReport();
        public List<ScorerByLeagueViewModel> LeagueReport(string league);
        public List<TopScorerViewModel> TopScorerReport();
        public List<CanadianScorerViewModel> Top5CCS();
        public void DownloadCsvFile();
    }
}
using Microsoft.AspNetCore.Mvc;
using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        IEnumerable<T> SaveToDatabase<T>(Stream file);
        Task<List<ScorerViewModel>> DatabaseReport();
        List<ScorerByLeagueViewModel> LeagueReport(string league);
        List<TopScorerViewModel> TopScorerReport();
        List<CanadianScorerViewModel> Top5CCS();
        Task DownloadCsvFile();
    }
}
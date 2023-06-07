using ScorersReporter.Models;

namespace ScorersReporter.Application
{
    public interface IScorersReporterApplication
    {
        Task<List<ScorerViewModel>> GetReportFromDatabase();
        List<ScorerByLeagueViewModel> GetScorersByLeagueReport(string league);
        List<CanadianScorerViewModel> GetTop5CanadianClassificationScorersReport();
        List<TopScorerViewModel> GetTopScorerReport();
        Task SaveScorersReportOnDesktop();
        void SaveScorersToDatabase(Stream file);
    }
}
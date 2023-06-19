using ScorersReporterApi.Models;
using ScorersReporterApi.Reponses;

namespace ScorersReporterApi;
public interface IScorersRepoterApi
{
    Task SaveFileToDatabase(ScorerFile scorerFile);
    Task<IEnumerable<ScorerResponse>> GetScorersReport();
    Task<IEnumerable<ScorerByLeagueResponse>> GetScorersByLeagueReport();
    Task<TopScorerResponse> GetTopScorerReport();
    Task<IEnumerable<CanadianScorerResponse>> GetTop5CanadiansClassificationScorersReport();
    Task SaveScorersReportOnDesktop();
}


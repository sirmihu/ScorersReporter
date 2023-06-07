using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IReportFromDatabase
    {
        Task<List<ScorerViewModel>> GetDbReport();
    }
}
using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IScorersDbService
    {
        void SaveToDatabase(IEnumerable<Scorer> scorers);
    }
}
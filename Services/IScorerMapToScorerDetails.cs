using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public interface IScorerMapToScorerDetails
    {
        List<ScorerDetails> ScorerDetails();
    }
}
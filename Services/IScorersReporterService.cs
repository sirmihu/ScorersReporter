
namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);
        public IEnumerable<dynamic> DbReport();
        public IEnumerable<dynamic> LeagueReport();
        public IEnumerable<dynamic> TopScorer();
        public IEnumerable<dynamic> Top5CCS();
    }
}
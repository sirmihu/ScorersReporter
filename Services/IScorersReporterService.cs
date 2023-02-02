
namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);
        public IEnumerable<dynamic> DbReport();
        public IEnumerable<dynamic> LeagueReport();
    }
}
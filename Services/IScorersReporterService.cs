using Microsoft.AspNetCore.Mvc;

namespace ScorersReporter.Services
{
    public interface IScorersReporterService
    {
        public IEnumerable<T> SaveToDatabase<T>(Stream file);
        public IAsyncEnumerable<dynamic> DbReport();
        public IEnumerable<dynamic> LeagueReport();
        public IEnumerable<dynamic> TopScorer();
        public IEnumerable<dynamic> Top5CCS();
        public FileContentResult DownloadCsvFile();
    }
}
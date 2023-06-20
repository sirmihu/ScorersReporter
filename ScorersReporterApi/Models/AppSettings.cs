namespace ScorersReporterApi.Models
{
    public class AppSettings
    {
        public string ScorersReporterApiBaseUrl { get; set; }
        public string SaveFileToDatabase { get; set; }
        public string SaveReportOnDesktop { get; set; }
        public string DownloadScorersReport { get; set; }
        public string GetScoresReport { get; set; }
        public string GetTop5CanadianClassificationScorers { get; set; }
        public string GetTopScorer { get; set; }
        public string GetScoresByLeagueReport { get; set; }
    }
}

using System;
namespace ScorersReporterApi.Models
{
	public class ScorersReporterApiUrl
	{
        public static string ScorersReporterApiBaseUrl = "https://localhost:7114";
        public static string SaveFileToDatabase = "Scorer/SaveFileToDatabase";
        public static string SaveReportOnDesktop = "Scorer/SaveScorersReportOnDesktop";
        public static string GetScoresReport = "Scorer/GetScorersReport";
        public static string GetTop5CanadianClassificationScorers = "Scorer/GetTop5CanadiansClassificationScorers";
        public static string GetTopScorer = "Scorer/GetTopScorer";
        public static string GetScoresByLeagueReport = "Scorer/GetScoresByLeagueReport";
    }
}


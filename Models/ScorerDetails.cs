using Microsoft.AspNetCore.Mvc;

namespace ScorersReporter.Models
{
    public class ScorerDetails
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(DateOfBirth.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                return age;
            }
        }
        public string Country { get; set; }
        public int Goals { get; set; }
        public int TotalGoals { get; set; }
        public int Assists { get; set; }
        public int TotalAssists { get; set; }
        public int Points
        {
            get
            {
                int goals = Goals;
                int assists = Assists;
                int points = goals + assists;
                return points;
            }
        }
        public string Club { get; set; }
        public string League { get; set; }
        public decimal MarketValue { get; set; }
        public decimal MarketValueEUR
        {
            get
            {
                return MarketValue;
            }
        }
        public decimal MarketValuePLN { get; set; }
    }

}

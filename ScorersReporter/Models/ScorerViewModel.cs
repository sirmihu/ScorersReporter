namespace ScorersReporter.Models
{
    public class ScorerViewModel 
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int TotalGoals { get; set; }
        public int TotalAssists { get; set; }
        public string Club { get; set; }
        public string League { get; set; }
        public decimal MarketValueEUR { get; set; }
        public decimal MarketValuePLN { get; set; }
    }

}

namespace ScorersReporter.Models
{
    public class ScorerViewModel 
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public int TotalGoals { get; set; }
        public int TotalAssists { get; set; }
        public decimal MarketValueEUR { get; set; }
        public decimal MarketValuePLN { get; set; }
        
        public void CalculateAge(DateTime dateOfBirth)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(dateOfBirth.ToString("yyyyMMdd"));
            Age = (now - dob) / 10000;
        }
    }

}

namespace ScorersReporter.Models
{
    public class ScorerViewModel : Scorer
    {
        private int _age;
        private decimal _marketValueEUR;
        private int _points;
        public string FullName { get; set; }
        public int Age
        {
            get
            {
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(DateOfBirth.ToString("yyyyMMdd"));
                _age = (now - dob) / 10000;
                return _age;
            }
            set
            {
                _age = value;
            }
        }
        public int TotalGoals { get; set; }
        public int TotalAssists { get; set; }
        public decimal MarketValueEUR
        {
            get
            {
                _marketValueEUR = MarketValue;
                return _marketValueEUR;
            }
            set
            {
                _marketValueEUR = value;
            }
        }
        public decimal MarketValuePLN { get; set; }
        public int Points
        {
            get
            {
                int goals = Goals;
                int assists = Assists;
                _points = goals + assists;
                return _points;
            }
            set
            {
                _points = value;
            }
        }
    }
}

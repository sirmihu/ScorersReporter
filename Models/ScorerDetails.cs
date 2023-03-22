using System.Reflection;

namespace ScorersReporter.Models
{
    public class ScorerDetails
    {
        private string _fullName;
        private int _age;
        private int _points;
        private decimal _marketValueEUR;
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string FullName
        {
            get
            {
                _fullName = FirstName + " " + LastName;
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
        public DateTime DateOfBirth { get; set; }
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
        public string Country { get; set; }
        public string League { get; set; }
        public string Club { get; set; }
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
                _points = goals + assists;
                return _points;
            }
            set
            {
                _points = value;
            }
        }
        public decimal MarketValue { get; set; }
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
    }
}


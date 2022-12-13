using CsvHelper.Configuration.Attributes;

namespace ScorersReporter.Models
{
    public class ScorersReport
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public string Club { get; set; }
        public string League { get; set; }
        public int MarketValue { get; set; }
    }
}

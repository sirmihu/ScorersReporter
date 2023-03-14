namespace ScorersReporter.Models
{
    public class ExchangeRatesSeries
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}

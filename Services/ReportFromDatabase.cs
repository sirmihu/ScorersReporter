using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public class ReportFromDatabase : IReportFromDatabase
    {
        private readonly RateExchange _rateExchange;
        private readonly IScorerMapToScorerDetails _scorerDetails;
        public ReportFromDatabase(RateExchange rateExchange, 
            IScorerMapToScorerDetails scorerDetails)
        {
            _rateExchange = rateExchange;
            _scorerDetails = scorerDetails;
        }
        public async Task<List<ScorerViewModel>> DbReport()
        {
            var scorersDetails = _scorerDetails.ScorerDetails().ToList();

            var rate = await _rateExchange.Rate();

            var records = scorersDetails.GroupBy(x => x.FullName)
                .Select(g => new ScorerViewModel
                {
                    FullName = g.Key,
                    Age = g.Select(s => s.Age).FirstOrDefault(),
                    Country = g.Select(s => s.Country).FirstOrDefault(),
                    TotalGoals = g.Sum(s => s.Goals),
                    TotalAssists = g.Sum(s => s.Assists),
                    Club = g.Select(s => s.Club).FirstOrDefault(),
                    League = g.Select(s => s.League).FirstOrDefault(),
                    MarketValueEUR = g.Select(s => s.MarketValueEUR).FirstOrDefault(),
                    MarketValuePLN = g.Select(s => s.MarketValueEUR * rate).FirstOrDefault()
                }).ToList();


            return records;

        }
    }
}

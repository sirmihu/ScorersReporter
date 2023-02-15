using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public class ReportFromDatabase
    {
        private readonly RateExchange _rateExchange;
        private readonly ScorerDetailsDtos _detailsDtos;
        public ReportFromDatabase(RateExchange rateExchange, ScorerDetailsDtos detailsDtos)
        {
            _rateExchange = rateExchange;
            _detailsDtos = detailsDtos;
        }
        public async Task<List<DbScorer>> DbReport()
        {
            var scorersDtos = _detailsDtos.ScorerDto().ToList();

            var rate = await _rateExchange.Rate();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new DbScorer
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

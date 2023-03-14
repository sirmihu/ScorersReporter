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
        public async Task<List<ScorerViewModel>> DbReport()
        {
            var scorersDtos = _detailsDtos.ScorerDto().ToList();

            var rate = await _rateExchange.Rate();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new ScorerViewModel
                {
                    FullName = g.Key,
                    FirstName = g.Select(s => s.FirstName).FirstOrDefault(),
                    LastName = g.Select(s => s.LastName).FirstOrDefault(),
                    Age = g.Select(s => s.Age).FirstOrDefault(),
                    DateOfBirth = g.Select(s => s.DateOfBirth).FirstOrDefault(),
                    Country = g.Select(s => s.Country).FirstOrDefault(),
                    TotalGoals = g.Sum(s => s.Goals),
                    TotalAssists = g.Sum(s => s.Assists),
                    Club = g.Select(s => s.Club).FirstOrDefault(),
                    League = g.Select(s => s.League).FirstOrDefault(),
                    MarketValue = g.Select(s => s.MarketValue).FirstOrDefault(),
                    MarketValueEUR = g.Select(s => s.MarketValueEUR).FirstOrDefault(),
                    MarketValuePLN = g.Select(s => s.MarketValueEUR * rate).FirstOrDefault()
                }).ToList();


            return records;

        }
    }
}

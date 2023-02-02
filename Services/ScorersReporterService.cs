using ScorersReporter.Models;
using ScorersReporter.Entities;

namespace ScorersReporter.Services
{
    public class ScorersReporterService : IScorersReporterService
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly FileReader _fileReader;
        private readonly RateExchange _rateExchange;
        private readonly ScorerDetailsDtos _detailsDtos;
        public ScorersReporterService(ScorersReportDbContext dbContext, FileReader fileReader, RateExchange rateExchange, ScorerDetailsDtos detailsDtos)
        {
            _dbContext = dbContext;
            _fileReader = fileReader;
            _rateExchange = rateExchange;
            _detailsDtos = detailsDtos;
        }

        public IEnumerable<T> SaveToDatabase<T>(Stream file)
        {
            var records = _fileReader.ReadCSV<T>(file);

            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Scorers.AddRange((IEnumerable<Scorer>)records);
                _dbContext.SaveChanges();
            }

            return records;
        }

        public IEnumerable<dynamic> DbReport()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            var rate = _rateExchange.Rate().FirstOrDefault();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new
                {
                    FullName = g.Key,
                    Age = g.Select(s => s.Age).FirstOrDefault(),
                    Country = g.Select(s => s.Country).FirstOrDefault(),
                    TotalGoals = g.Sum(s => s.Goals),
                    TotalAssists = g.Sum(s => s.Assists),
                    Club = g.Select(s => s.Club).FirstOrDefault(),
                    Leauge = g.Select(s => s.League).FirstOrDefault(),
                    MarketValueEUR = g.Select(s => s.MarketValueEUR).FirstOrDefault(),
                    MarketVaulePLN = g.Select(s => s.MarketValueEUR * rate).FirstOrDefault()
                }).ToList();

            return records;
        }

        public IEnumerable<dynamic> LeagueReport()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            var records = scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new
                {
                    Leauge = g.Select(s => s.League).FirstOrDefault(),
                    FullName = g.Key,
                    Age = g.Select(s => s.Age).FirstOrDefault(),
                    Country = g.Select(s => s.Country).FirstOrDefault(),
                    TotalGoals = g.Sum(s => s.Goals),
                    TotalAssists = g.Sum(s => s.Assists),
                    Club = g.Select(s => s.Club).FirstOrDefault()
                })
                .OrderBy(g => g.Leauge)
                .ToList();

            return records;
        }

        public IEnumerable<dynamic> TopScorer()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            return scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new
                {
                    FullName = g.Key,
                    TotalGoals = g.Sum(s => s.Goals)
                })
                .OrderByDescending(g => g.TotalGoals)
                .Take(1)
                .ToList();
        }

        public IEnumerable<dynamic> Top5CCS()
        {
            var scorersDtos = _detailsDtos.ScorerDto();

            return scorersDtos.GroupBy(x => x.FullName)
                .Select(g => new
                {
                    FullName = g.Key,
                    Points = g.Sum(s => s.Points)
                })
                .OrderByDescending(g => g.Points)
                .Take(5)
                .ToList();
        }

    }
}

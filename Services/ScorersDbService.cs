using Microsoft.EntityFrameworkCore;
using ScorersReporter.Entities;
using ScorersReporter.Models;

namespace ScorersReporter.Services
{
    public class ScorersDbService : IScorersDbService
    {
        private readonly ScorersReportDbContext _dbContext;

        public ScorersDbService(ScorersReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveToDatabase(IEnumerable<Scorer> scorers)
        {
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Scorers.AddRange(scorers);
                _dbContext.SaveChanges();
            }
        }
    }
}

using ScorersReporter.Models;
using ScorersReporter.Entities;
using AutoMapper;

namespace ScorersReporter.Services
{
    public class ScorersReporterService : IScorersReporterService
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FileReader _fileReader;
        public ScorersReporterService(ScorersReportDbContext dbContext, IMapper mapper, FileReader fileReader)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileReader = fileReader;
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

    }
}


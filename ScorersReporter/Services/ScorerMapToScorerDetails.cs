using ScorersReporter.Models;
using ScorersReporter.Entities;
using AutoMapper;

namespace ScorersReporter.Services
{
    public class ScorerMapToScorerDetails : IScorerMapToScorerDetails
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly IMapper _mapper;
        public ScorerMapToScorerDetails(ScorersReportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ScorerDetails> ScorerDetails()
        {
            var scorer = _dbContext.Scorers.ToList();

            var scorerDetails = _mapper.Map<List<ScorerDetails>>(scorer);

            return scorerDetails;
        }

    }
}

/*using ScorersReporter.Models;
using ScorersReporter.Entities;
using AutoMapper;
using System.Runtime.Serialization.Formatters.Binary;

namespace ScorersReporter.Services
{
    public class ScorerDetailsDtos
    {
        private readonly ScorersReportDbContext _dbContext;
        private readonly IMapper _mapper;
        public ScorerDetailsDtos(ScorersReportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ScorerViewModel> ScorerDto()
        {
            var scorers = _dbContext.Scorers.ToList();

            var scorersDtos = _mapper.Map<List<ScorerViewModel>>(scorers);

            return scorersDtos;
        }
        
    }
}
*/
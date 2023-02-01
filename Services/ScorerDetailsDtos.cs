﻿using ScorersReporter.Models;
using ScorersReporter.Entities;
using AutoMapper;

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

        public List<ScorerDetails> ScorerDto()
        {
            var scorers = _dbContext.Scorers.ToList();

            var scorersDtos = _mapper.Map<List<ScorerDetails>>(scorers);

            return scorersDtos;
        }
    }
}

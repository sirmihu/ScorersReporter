using AutoMapper;
using ScorersReporter.Models;

namespace ScorersReporter
{
    public class ScorersMappingProfile : Profile
    {
        public ScorersMappingProfile()
        {
            CreateMap<Scorer, ScorerDetails>();
        }
 
    }
 }


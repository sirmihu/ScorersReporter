using AutoMapper;
using ScorersReporter.Models;

namespace ScorersReporter
{
    public class ScorersMappingProfile : Profile
    {
        public ScorersMappingProfile()
        {
            CreateMap<Scorer, ScorerDetails>()
                .ForMember(m => m.FullName, c => c.MapFrom(s => s.FirstName + " " + s.LastName));
            CreateMap<ScorerDetails, DbScorer>()
                .ForMember(m => m.FullName, c => c.MapFrom(s => s.FullName));
            
        }
    }
}


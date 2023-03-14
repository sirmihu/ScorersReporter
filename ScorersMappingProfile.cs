using AutoMapper;
using ScorersReporter.Models;

namespace ScorersReporter
{
    public class ScorersMappingProfile : Profile
    {
        public ScorersMappingProfile()
        {
            CreateMap<Scorer, ScorerViewModel>();
            CreateMap<Scorer, ScorerViewModel>()
                .ForMember(m => m.FullName, c => c.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}


using System;
using AutoMapper;
using ScorersReporterApi.Reponses;
using ScorersReporterWebApp.Models;

namespace ScorersReporterWebApp.Automapper
{
    public class ScorersViewModelMappingProfile : Profile
    {
        public ScorersViewModelMappingProfile()
        {
            CreateMap<ScorerResponse, ScorerViewModel>();
            CreateMap<TopScorerResponse, TopScorerViewModel>();
            CreateMap<CanadianScorerResponse, CanadianScorerViewModel>();
        }
    }
}


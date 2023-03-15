using AutoMapper;
using ScorersReporter.Models;

namespace ScorersReporter
{
    public class ScorersMappingProfile : Profile
    {
        public ScorersMappingProfile()
        {
            CreateMap<Scorer, ScorerViewModel>()
                .ConstructUsing((source, destination) => 
                {
                    int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    int dob = int.Parse(source.DateOfBirth.ToString("yyyyMMdd"));
                    var age = (now - dob) / 10000;

                    return new ScorerViewModel 
                    {
                        FullName = source.FirstName + " " + source.LastName,
                        Age = age,
                        TotalGoals = 
                    }
                })
        }
    }
}


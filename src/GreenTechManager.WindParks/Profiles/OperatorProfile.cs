using AutoMapper;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class OperatorProfile : Profile
    {
        public OperatorProfile()
        {
            CreateMap<OperatorModel, Operator>().ReverseMap();
        }
    }
}

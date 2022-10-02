using AutoMapper;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;

namespace GreenTechManager.SolarParks.Profiles
{
    public class OperatorProfile : Profile
    {
        public OperatorProfile()
        {
            CreateMap<OperatorModel, Operator>();
        }
    }
}

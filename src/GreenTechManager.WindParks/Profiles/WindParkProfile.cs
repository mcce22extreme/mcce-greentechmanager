using AutoMapper;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class WindParkProfile : Profile
    {
        public WindParkProfile()
        {
            CreateMap<WindPark, WindParkListModel>()
                .ForMember(d => d.OperatorName, opt => opt.MapFrom(s => s.Operator.Name));

            CreateMap<WindParkModel, WindPark>();
        }
    }
}

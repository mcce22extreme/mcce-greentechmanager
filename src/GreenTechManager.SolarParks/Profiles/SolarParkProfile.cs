using AutoMapper;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class SolarParkProfile : Profile
    {
        public SolarParkProfile()
        {
            CreateMap<SolarParkModel, SolarPark>();

            CreateMap<SolarPark, SolarParkListModel>();
        }
    }
}

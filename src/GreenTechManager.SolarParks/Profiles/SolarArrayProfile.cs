using AutoMapper;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class SolarArrayProfile : Profile
    {
        public SolarArrayProfile()
        {
            CreateMap<SolarArray, SolarArrayListModel>();

            CreateMap<SolarParkModel, SolarArray>();
        }
    }
}

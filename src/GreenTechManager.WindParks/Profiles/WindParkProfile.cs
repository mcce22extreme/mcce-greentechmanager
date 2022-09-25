using AutoMapper;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class WindParkProfile : Profile
    {
        public WindParkProfile()
        {
            CreateMap<WindPark, WindParkListModel>();

            CreateMap<WindParkModel, WindPark>();
        }
    }
}

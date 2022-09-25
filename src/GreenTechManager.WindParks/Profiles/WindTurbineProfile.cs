using AutoMapper;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class WindTurbineProfile : Profile
    {
        public WindTurbineProfile()
        {
            CreateMap<WindTurbine, WindTurbineListModel>();

            CreateMap<WindTurbineModel, WindTurbine>();
        }
    }
}

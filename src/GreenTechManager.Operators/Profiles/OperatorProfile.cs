using AutoMapper;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Messages;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Profiles
{
    public class OperatorProfile : Profile
    {
        public OperatorProfile()
        {
            CreateMap<Operator, OperatorListModel>();

            CreateMap<OperatorModel, Operator>();

            CreateMap<Operator, OperatorSavedMessage>();

            CreateMap<Operator, OperatorDeletedMessage>();
        }
    }
}

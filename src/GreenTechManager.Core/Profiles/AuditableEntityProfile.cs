using AutoMapper;
using GreenTechManager.Core.Entities;
using GreenTechManager.Core.Messages;

namespace GreenTechManager.Core.Profiles
{
    public class AuditableEntityProfile : Profile
    {
        public AuditableEntityProfile()
        {
            CreateMap<AuditableEntityBase, EntityMessage>()
                .ForMember(d => d.EntityId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.EntityName, opt => opt.MapFrom(s => s.EntityName))
                .ForMember(d => d.EntityType, opt => opt.MapFrom(s => s.GetType().Name));
        }
    }
}

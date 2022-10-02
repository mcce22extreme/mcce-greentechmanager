using AutoMapper;
using GreenTechManager.Core.Entities;
using GreenTechManager.Core.Models;

namespace GreenTechManager.Core.Profiles
{
    public class AuditEntryProfile : Profile
    {
        public AuditEntryProfile()
        {
            CreateMap<AuditEntry, AuditEntryModel>();
        }
    }
}

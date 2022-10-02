using AutoMapper;
using GreenTechManager.Core.Managers;

namespace GreenTechManager.SolarParks.Managers
{
    public class AuditEntryManager : AuditEntryManagerBase
    {
        public AuditEntryManager(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}

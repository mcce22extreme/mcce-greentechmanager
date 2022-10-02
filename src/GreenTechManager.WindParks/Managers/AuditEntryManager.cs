using AutoMapper;
using GreenTechManager.Core.Managers;

namespace GreenTechManager.WindParks.Managers
{
    public class AuditEntryManager : AuditEntryManagerBase
    {
        public AuditEntryManager(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}

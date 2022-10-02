using AutoMapper;
using GreenTechManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Core.Managers
{
    public interface IAuditEntryManager
    {
        Task<AuditEntryModel[]> GetAuditEntries();
    }

    public abstract class AuditEntryManagerBase : IAuditEntryManager
    {
        private readonly AuditableDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuditEntryManagerBase(AuditableDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuditEntryModel[]> GetAuditEntries()
        {
            var entries = await _dbContext.AuditEntries.ToListAsync();

            return entries.Select(_mapper.Map<AuditEntryModel>).ToArray();
        }
    }
}

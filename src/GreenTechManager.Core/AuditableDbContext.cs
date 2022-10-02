using GreenTechManager.Core.Entities;
using GreenTechManager.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Core
{
    public abstract class AuditableDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<AuditEntry> AuditEntries { get; set; }

        public AuditableDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _httpContextAccessor = contextAccessor;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await AddAuditInfo();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AddAuditInfo().Wait();

            return base.SaveChanges();
        }

        private async Task AddAuditInfo()
        {
            var entries = ChangeTracker.Entries<AuditableEntityBase>()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .ToList();

            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";

            foreach (var entry in entries)
            {
                var operation = AuditOperation.None;

                switch (entry.State)
                {
                    case EntityState.Added:
                        operation = AuditOperation.Created;
                        entry.Entity.Creator = userName;
                        entry.Entity.CreatedUtc = DateTime.UtcNow;

                        break;
                    case EntityState.Modified:
                        operation = AuditOperation.Modified;
                        entry.Entity.Modifier = userName;
                        entry.Entity.ModifiedUtc = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        operation = AuditOperation.Deleted;
                        break;
                }

                var auditEntry = new AuditEntry
                {
                    EntityId = entry.Entity.Id,
                    EntityName = entry.Entity.EntityName,
                    EntityType = entry.Entity.GetType().Name,
                    Operation = operation,                 
                    DateUtc = DateTime.UtcNow,
                    UserName = userName,
                };

                await AuditEntries.AddAsync(auditEntry);
            }
        }        
    }
}

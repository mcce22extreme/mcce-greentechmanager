using GreenTechManager.Core;
using GreenTechManager.WindParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Operators
{
    public class AppDbContext : AuditableDbContext
    {
        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
        }
    }
}

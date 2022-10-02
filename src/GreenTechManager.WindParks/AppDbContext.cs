using GreenTechManager.Core;
using GreenTechManager.WindParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.WindParks
{
    public class AppDbContext : AuditableDbContext
    {
        public DbSet<WindPark> WindParks { get; set; }

        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions opt, IHttpContextAccessor httpContextAccessor)
            : base(opt, httpContextAccessor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>()
                .HasMany(x => x.WindParks)
                .WithOne(x => x.Operator)
                .IsRequired();
        }
    }
}

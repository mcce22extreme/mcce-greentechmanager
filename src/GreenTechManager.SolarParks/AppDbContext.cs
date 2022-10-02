using GreenTechManager.Core;
using GreenTechManager.SolarParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.SolarParks
{
    public class AppDbContext : AuditableDbContext
    {
        public DbSet<SolarPark> SolarParks { get; set; }

        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions opt, IHttpContextAccessor httpContextAccessor)
            : base(opt, httpContextAccessor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>()
                .HasMany(x => x.SolarParks)
                .WithOne(x => x.Operator)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

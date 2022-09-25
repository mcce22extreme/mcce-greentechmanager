using GreenTechManager.SolarParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.SolarParks
{
    public class AppDbContext : DbContext
    {
        public DbSet<SolarPark> SolarParks { get; set; }

        public DbSet<SolarArray> SolarArrays { get; set; }        

        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt)
            : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolarPark>()
                .HasMany(x => x.SolarArrays)
                .WithOne( x=> x.SolarPark)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Operator>()
                .HasMany(x => x.SolarParks)
                .WithOne(x => x.Operator)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

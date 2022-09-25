using GreenTechManager.WindParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.WindParks
{
    public class AppDbContext : DbContext
    {
        public DbSet<WindPark> WindParks { get; set; }

        public DbSet<WindTurbine> WindTurbines { get; set; }

        public DbSet<Operator> Operators { get; set; }


        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WindPark>()
                .HasMany(x => x.WindTurbines)
                .WithOne(x => x.WindPark)
                .IsRequired();

            modelBuilder.Entity<Operator>()
                .HasMany(x => x.WindParks)
                .WithOne(x => x.Operator)
                .IsRequired();
        }
    }
}

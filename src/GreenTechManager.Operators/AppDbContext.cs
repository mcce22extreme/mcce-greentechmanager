using GreenTechManager.WindParks.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Operators
{
    public class AppDbContext : DbContext
    {
        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}

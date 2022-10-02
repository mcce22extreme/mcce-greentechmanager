using GreenTechManager.WindParks.Entities;

namespace GreenTechManager.WindParks
{
    public static class DataSeed
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            // Seed data
            dbContext.Operators.Add(new Operator
            {
                ExternalId = 1,
                Name = "Burgenland Energie AG",
            });

            dbContext.WindParks.Add(new WindPark
            {
                Name = "Windpark Andau/Halbturn",
                OperatorId = 1,
                StartOfOperation = new DateTime(2014, 1, 1),
                MaxPowerOutput = 237,
                NumberOfTurbines= 79,
                Location = "47° 49′ 48″ N, 17° 1′ 20″ O"
            });

            dbContext.SaveChanges();
        }
    }
}

using GreenTechManager.SolarParks.Entities;

namespace GreenTechManager.SolarParks
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

            dbContext.SolarParks.Add(new SolarPark
            {
                Name = "Solarpark Nickelsdorf",
                OperatorId = 1,
                StartOfOperation = new DateTime(2024, 1, 1),
                Location = "47° 49′ 48″ N, 17° 1′ 20″ O"
            });

            for (int i = 0; i < 120; i++)
            {
                dbContext.SolarArrays.Add(new SolarArray
                {
                    Type = "Nuuoko",
                    SolarParkId = 1,
                    Location = "47° 49′ 48″ N, 17° 1′ 20″ O",
                    PowerRating = 750000,
                    NumberOfPanels = 35,
                    Size = 240                    
                });
            }

            dbContext.SaveChanges();
        }
    }
}

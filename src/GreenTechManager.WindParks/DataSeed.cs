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
                Location = "47° 49′ 48″ N, 17° 1′ 20″ O"
            });

            var hubHeights = new [] { 92,122,135,149 };
            var random = new Random();

            for (int i = 0; i < 79; i++)
            {
                dbContext.WindTurbines.Add(new WindTurbine
                {
                    Type = "Enercon E-101",
                    WindParkId = 1,
                    Location = "47° 49′ 48″ N, 17° 1′ 20″ O",
                    PowerOutput = 3050000,
                    RotorDiameter = 101,
                    HubHeight = hubHeights[random.Next(0, 3)]
                });
            }

            dbContext.SaveChanges();
        }
    }
}

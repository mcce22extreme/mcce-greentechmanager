using GreenTechManager.WindParks.Entities;

namespace GreenTechManager.Operators
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
                Name = "Burgenland Energie AG",
                Address = "Kasernenstraße 9",
                City = "Eisenstadt",
                Zip = 7000,
                Country = "Österreich"
            });

            dbContext.SaveChanges();
        }
    }
}

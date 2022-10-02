using System.Security.Claims;
using GreenTechManager.Identity.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GreenTechManager.Identity
{
    public static class DataSeed
    {
        private const string PASSWORD = "Greentech!1234";

        private const string ROLE_WINDPARK_ADMIN = "WindParkAdmin";
        private const string ROLE_WINDPARK_USER = "WindParkUser";

        private const string ROLE_SOLARPARK_ADMIN = "SolarParkAdmin";
        private const string ROLE_SOLARPARK_USER = "SolarParkUser";

        private const string ROLE_OPERATOR_ADMIN = "OperatorAdmin";
        private const string ROLE_OPERATOR_USER = "OperatorUser";

        private const string ROLE_AUDIT_READER = "AuditReader";

        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed data
            CreateUser(userManager, "windparkadmin", PASSWORD, ROLE_WINDPARK_ADMIN, ROLE_WINDPARK_USER, ROLE_AUDIT_READER);
            CreateUser(userManager, "windparkuser", PASSWORD, ROLE_WINDPARK_USER);

            CreateUser(userManager, "solarparkadmin", PASSWORD, ROLE_SOLARPARK_ADMIN, ROLE_SOLARPARK_USER, ROLE_AUDIT_READER);
            CreateUser(userManager, "solarparkuser", PASSWORD, ROLE_SOLARPARK_USER);

            CreateUser(userManager, "operatoradmin", PASSWORD, ROLE_OPERATOR_ADMIN, ROLE_OPERATOR_USER, ROLE_AUDIT_READER);
            CreateUser(userManager, "operatoruser", PASSWORD, ROLE_OPERATOR_USER);

            dbContext.SaveChanges();
        }

        private static void CreateUser(UserManager<ApplicationUser> userManager, string userName, string password, params string[] roles)
        {
            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = $"{userName}@greentech.com",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, password).Wait();

                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, userName));

                foreach(var role in roles)
                {
                    userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, role));
                }
            }
        }        
    }
}

using GreenTechManager.Core;
using GreenTechManager.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Identity
{
    public class Bootstrap : BootstrapBase
    {
        protected override WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
        {
            var builder = base.CreateWebApplicationBuilder(args);

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("identitydb"));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer(opt =>
            {
                opt.Events.RaiseErrorEvents = true;
                opt.Events.RaiseInformationEvents = true;
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseSuccessEvents = true;
                opt.EmitStaticAudienceClaim = true;
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryApiScopes(Config.Scopes)
                .AddAspNetIdentity<ApplicationUser>();

            return builder;
        }

        protected override WebApplication CreateApplication(string[] args)
        {
            var app = base.CreateApplication(args);

            app.UseIdentityServer();

            DataSeed.Seed(app);

            return app;
        }
    }
}

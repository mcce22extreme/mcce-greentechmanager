using GreenTechManager.Core;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Managers;
using GreenTechManager.SolarParks.Managers;
using GreenTechManager.SolarParks.Processors;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.SolarParks
{
    public class Bootstrap : ApiBootstrapBase
    {
        protected override WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
        {
            var builder = base.CreateWebApplicationBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("solarparkdb"));

            builder.Services.AddScoped<ISolarParkManager, SolarParkManager>();

            builder.Services.AddScoped<IOperatorManager, OperatorManager>();

            builder.Services.AddScoped<IAuditEntryManager, AuditEntryManager>();

            builder.Services.AddHostedService(x => ActivatorUtilities.CreateInstance<EntityEventProcessor>(x, AppSettings.Current.MessageBus.HostName, AppSettings.Current.MessageBus.Port));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.RequireUserRolePolicy, p => p.RequireRole("SolarParkUser"));
                options.AddPolicy(AuthConstants.RequireAdminRolePolicy, p => p.RequireRole("SolarParkAdmin"));
            });

            return builder;
        }

        protected override WebApplication CreateApplication(string[] args)
        {
            var app = base.CreateApplication(args);

            DataSeed.Seed(app);

            return app;
        }
    }
}

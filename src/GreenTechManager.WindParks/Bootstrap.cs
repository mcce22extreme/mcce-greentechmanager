using GreenTechManager.Core;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Managers;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Processors;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.WindParks
{
    public class Bootstrap : ApiBootstrapBase
    {
        protected override WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
        {
            var builder = base.CreateWebApplicationBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("windparkdb"));

            builder.Services.AddScoped<IWindParkManager, WindParkManager>();

            builder.Services.AddScoped<IOperatorManager, OperatorManager>();

            builder.Services.AddScoped<IAuditEntryManager, AuditEntryManager>();

            builder.Services.AddHostedService(x => ActivatorUtilities.CreateInstance<EntityEventProcessor>(x, AppSettings.Current.MessageBus.HostName, AppSettings.Current.MessageBus.Port));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.RequireUserRolePolicy, p => p.RequireRole("WindParkUser"));
                options.AddPolicy(AuthConstants.RequireAdminRolePolicy, p => p.RequireRole("WindParkAdmin"));
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

using GreenTechManager.Core;
using GreenTechManager.Core.Services;
using GreenTechManager.Operators.Managers;
using GreenTechManager.WindParks.Constants;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Operators
{
    public class Bootstrap : BootstrapBase
    {
        protected override WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
        {
            var builder = base.CreateWebApplicationBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("operatordb"));

            builder.Services.AddSingleton<IMessageBusService>(x => new MessageBusService(AppSettings.Current.MessageBus.HostName, AppSettings.Current.MessageBus.Port));

            builder.Services.AddScoped<IOperatorManager, OperatorManager>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.RequireUserRolePolicy, p => p.RequireRole("OperatorUser"));
                options.AddPolicy(AuthConstants.RequireAdminRolePolicy, p => p.RequireRole("OperatorAdmin"));
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

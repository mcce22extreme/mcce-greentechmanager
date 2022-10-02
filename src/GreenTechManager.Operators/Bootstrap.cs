using GreenTechManager.Core;
using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Managers;
using GreenTechManager.Operators.Managers;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Operators
{
    public class Bootstrap : ApiBootstrapBase
    {
        protected override WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
        {
            var builder = base.CreateWebApplicationBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("operatordb"));

            builder.Services.AddScoped<IOperatorManager, OperatorManager>();

            builder.Services.AddScoped<IAuditEntryManager, AuditEntryManager>();

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

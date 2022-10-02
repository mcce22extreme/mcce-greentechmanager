//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
//    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
//    .AddEnvironmentVariables();

//builder.Services.AddOcelot(builder.Configuration);

//builder.WebHost.UseUrls("http://apigateway:80");

//var app = builder.Build();

//app.UseOcelot();

//app.Run();

namespace GreenTechManager.ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Bootstrap().Run(args);
        }
    }
}


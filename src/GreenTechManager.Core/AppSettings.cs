using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace GreenTechManager.Core
{
    public class AppSettings
    {
        static AppSettings()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Current = Config.Get<AppSettings>();
        }

        public static AppSettings Current { get; }

        public static IConfigurationRoot Config { get; }

        public string BaseAddress { get; set; }

        public string AuthorityEndpoint { get; set; }

        public string ConnectionString { get; set; }
                
        public MessageBusConfig MessageBus { get; set; }
    }

    public class MessageBusConfig
    {
        public string HostName { get; set; }

        public int Port { get; set; }
    }
}

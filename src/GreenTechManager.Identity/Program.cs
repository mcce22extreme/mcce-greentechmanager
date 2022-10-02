using IdentityServer4.Test;

namespace GreenTechManager.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Bootstrap().Run(args);
        }
    }
}

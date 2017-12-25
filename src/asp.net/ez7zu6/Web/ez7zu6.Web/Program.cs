using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ez7zu6.Web.Helper;

namespace ez7zu6.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isAppHarbor = (new EnvironmentHelper()).IsAppHarbor;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var config = builder.Build();

            var environmentName = isAppHarbor ? "AppHarbor" : "Development";
            BuildWebHost(args, environmentName).Run();
        }

        public static IWebHost BuildWebHost(string[] args, string environmentName) => 
            WebHost.CreateDefaultBuilder(args) // TODO: this line is using the {env.EnvironmentName}.json - need to break this apart
                .UseEnvironment(environmentName)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}

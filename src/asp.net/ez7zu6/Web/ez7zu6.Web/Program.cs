using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ez7zu6.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: need to DRY
            bool.TryParse(System.Environment.GetEnvironmentVariable("IS_APPHARBOR"), out bool isAppHarbor);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.AppHarbor.json", isAppHarbor, true);

            var config = builder.Build();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args) // this line is using the {env.EnvironmentName}.json - need to break this apart
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}

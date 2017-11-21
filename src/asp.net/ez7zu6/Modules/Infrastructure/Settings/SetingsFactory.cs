using System.IO;
using ez7zu6.Core;

namespace ez7zu6.Infrastructure.Settings
{
    public class SetingsFactory
    {
        private readonly string EnvironmentPath = "env";
        private readonly string WeatherKey = null;

        public ISettings GetSettings(IAppEnvironment appEnvironment)
        {
            var appRootPath = appEnvironment.AppRootPath ?? string.Empty;
            if (appEnvironment.Location == EnvironmentLocation.AppHarbor)
                return new AppHarborSettings(WeatherKey, null);
            else
            {
                var configurationPath = Path.Combine(appRootPath, EnvironmentPath, "ConnectionString.txt");
                return new LocalSettings(WeatherKey, configurationPath);
            }
        }

    }
}

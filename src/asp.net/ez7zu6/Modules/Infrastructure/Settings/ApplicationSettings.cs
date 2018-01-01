using ez7zu6.Core;

namespace ez7zu6.Infrastructure.Settings
{
    public class ApplicationSettings
    {
        public IAppEnvironment AppEnvironment { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
    }
}

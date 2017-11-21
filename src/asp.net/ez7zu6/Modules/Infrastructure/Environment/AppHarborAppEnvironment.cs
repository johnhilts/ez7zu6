using ez7zu6.Core;

namespace ez7zu6.Infrastructure.Environment
{
    public class AppHarborAppEnvironment : IAppEnvironment
    {
        public EnvironmentLocation Location => EnvironmentLocation.AppHarbor;
        public string AppRootPath { get => null; }
    }
}

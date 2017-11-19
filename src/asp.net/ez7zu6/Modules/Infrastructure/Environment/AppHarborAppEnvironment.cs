using Core;

namespace Infrastructure.Environment
{
    public class AppHarborAppEnvironment : IAppEnvironment
    {
        public EnvironmentLocation Location => EnvironmentLocation.AppHarbor;
        public string AppRootPath { get => null; }
    }
}

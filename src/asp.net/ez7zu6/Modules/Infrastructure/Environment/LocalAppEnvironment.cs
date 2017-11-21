using ez7zu6.Core;

namespace ez7zu6.Infrastructure.Environment
{
    public class LocalAppEnvironment : IAppEnvironment
    {
        public EnvironmentLocation Location => EnvironmentLocation.Local;

        private readonly string _rootPath;
        public string AppRootPath { get => _rootPath; }

        public LocalAppEnvironment(string rootPath) => _rootPath = rootPath;
    }
}

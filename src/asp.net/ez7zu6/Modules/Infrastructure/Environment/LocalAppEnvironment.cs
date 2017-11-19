using Core;

namespace Infrastructure.Environment
{
    public class LocalAppEnvironment : IAppEnvironment
    {
        private readonly string _rootPath;

        public LocalAppEnvironment(string rootPath) => _rootPath = rootPath;

        public string AppRootPath { get => _rootPath; }
    }
}

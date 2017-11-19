namespace Core
{
    public interface IAppEnvironment
    {
        EnvironmentLocation Location { get; }
        string AppRootPath { get; }
    }
}

namespace ez7zu6.Core
{
    public interface IAppEnvironment
    {
        EnvironmentLocation Location { get; }
        string AppRootPath { get; }
    }
}

namespace ez7zu6.Infrastructure.Settings
{
    public class LocalSettings : BaseSettings
    {
        public LocalSettings(string weatherKeyPath, string configurationPath) : base(weatherKeyPath, configurationPath)
        {
        }

        protected override string GetConnectionString() => System.IO.File.ReadAllText(_configurationPath);
    }
}

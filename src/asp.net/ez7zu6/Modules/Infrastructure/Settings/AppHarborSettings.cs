namespace Infrastructure.Settings
{
    public class AppHarborSettings : BaseSettings
    {
        public AppHarborSettings(string weatherKeyPath, string configurationPath) : base(weatherKeyPath, configurationPath)
        {
        }

        protected override string GetConnectionString() => System.Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");
    }
}

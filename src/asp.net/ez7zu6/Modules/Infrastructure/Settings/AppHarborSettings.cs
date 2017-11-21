using System;

namespace ez7zu6.Infrastructure.Settings
{
    public class AppHarborSettings : BaseSettings
    {
        public AppHarborSettings(string weatherKeyPath, string configurationPath) : base(weatherKeyPath, configurationPath)
        {
        }

        protected override string GetConnectionString()
        {
            var connectionString = System.Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Environment Variable not Set");

            return connectionString;
        }
    }
}

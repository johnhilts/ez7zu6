using System;

namespace Infrastructure
{
    public class Settings
    {
        public Settings(string weatherKeyPath, string configurationPath)
        {
            WeatherKeyPath = weatherKeyPath;
            _configurationPath = configurationPath;
        }

        public string WeatherKeyPath { get; private set; }
        private string _configurationPath;

        private string _connectionString;
        public string ConnectionString()
        {
            return _connectionString ?? (_connectionString = GetConnectionString());
        }

        private string GetConnectionString()
        {
            var connectionStringFromEnvironment = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");
            return string.IsNullOrWhiteSpace(connectionStringFromEnvironment) ? System.IO.File.ReadAllText(_configurationPath) : connectionStringFromEnvironment;
        }
    }
}

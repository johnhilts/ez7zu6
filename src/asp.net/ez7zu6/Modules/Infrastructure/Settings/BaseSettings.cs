using ez7zu6.Core;

namespace ez7zu6.Infrastructure.Settings
{
    public abstract class BaseSettings : ISettings
    {
        public BaseSettings(string weatherKeyPath, string configurationPath)
        {
            WeatherKeyPath = weatherKeyPath;
            _configurationPath = configurationPath;
        }

        public string WeatherKeyPath { get; private set; }
        protected string _configurationPath;

        private string _connectionString;
        public string ConnectionString => _connectionString ?? (_connectionString = GetConnectionString());

        protected abstract string GetConnectionString();
    }
}

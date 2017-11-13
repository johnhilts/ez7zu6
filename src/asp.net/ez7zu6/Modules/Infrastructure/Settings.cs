namespace Infrastructure
{
    public class Settings
    {
        //public Settings(string keyPath, string configurationPath)
        //{
        //    KeyPath = keyPath;
        //    _configurationPath = configurationPath;
        //}

        public string KeyPath { get; private set; }
        private string _configurationPath;

        private string _connectionString;
        public string ConnectionString()
        {
            return _connectionString ?? (_connectionString = GetConnectionString());
        }

        private string GetConnectionString()
        {
            //var connectionStringFromConfig = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
            //return string.IsNullOrWhiteSpace(connectionStringFromConfig) ? System.IO.File.ReadAllText(_configurationPath) : connectionStringFromConfig;
            return "Data Source=.;Initial Catalog=ez7zu6;user id=ez_user;password=ez_123!!;Type System Version=SQL Server 2012;Pooling=False";
        }
    }
}

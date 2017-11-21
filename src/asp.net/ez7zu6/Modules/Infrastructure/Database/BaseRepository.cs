using System.Data.SqlClient;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Settings;

namespace ez7zu6.Infrastructure.Database
{
    public class BaseRepository
    {
        protected readonly ISettings _settings;

        public BaseRepository(IAppEnvironment appEnvironment) => _settings = new SetingsFactory().GetSettings(appEnvironment);

        protected SqlConnection GetConnection() => new SqlConnection(_settings.ConnectionString);
    }
}
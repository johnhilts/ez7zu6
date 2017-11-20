using System.Data.SqlClient;
using Core;
using Infrastructure.Settings;

namespace Infrastructure.Database
{
    public class BaseRepository
    {
        protected readonly ISettings _settings;

        public BaseRepository(IAppEnvironment appEnvironment) => _settings = new SetingsFactory().GetSettings(appEnvironment);

        protected SqlConnection GetConnection() => new SqlConnection(_settings.ConnectionString);
    }
}
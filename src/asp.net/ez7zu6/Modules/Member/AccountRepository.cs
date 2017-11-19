using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Core;
using Infrastructure.Settings;

namespace Member
{
    public class AccountRepository
    {
        private readonly ISettings _settings;

        public AccountRepository(IAppEnvironment appEnvironment) => _settings = new SetingsFactory().GetSettings(appEnvironment);

        private SqlConnection GetConnection() => new SqlConnection(_settings.ConnectionString);

        public async Task<AccountQueryModel> GetAccountInfoByLoginPasswordAsync(string username, string password)
        {
            using (var db = GetConnection())
            {
                // todo: add password check too!
                const string query = @"
select Username, UserPassword
from dbo.Accounts (nolock) 
where Username = @Username 
and IsActive = 1";

                var model = await db.QueryAsync<AccountQueryModel>(query, new { Username = username, });

                return model.SingleOrDefault() ?? new AccountQueryModel { NoMatch = true };
            }
        }

    }
}

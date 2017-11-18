using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Infrastructure;

namespace Member
{
    public class AccountRepository
    {
        private readonly Settings _settings;

        public AccountRepository(Settings settings)
        {
            _settings = settings;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_settings.ConnectionString());
        }

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

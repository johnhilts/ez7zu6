using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Database;
using ez7zu6.Data.Models.Account;

namespace ez7zu6.Data.Repositories
{
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(IAppEnvironment appEnvironment) : base(appEnvironment) { }

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

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

        public async Task<AccountQueryModel> GetUserInfoByUsernameAndPassword(string username, string password)
        {
            using (var db = GetConnection())
            {
                const string query = @"
select UserId, Username, UserPassword
from dbo.Accounts (nolock) 
where Username = @Username 
and IsActive = 1";

                var model = await db.QueryAsync<AccountQueryModel>(query, new { Username = username, });

                return model.SingleOrDefault() ?? new AccountQueryModel { NoMatch = true };
            }
        }

        public async Task AddUser(AccountCreateModel dataModel)
        {
            using (var db = GetConnection())
            {
                string sql = @"
                insert into dbo.Accounts(UserId, Username, UserPassword, IsAnonymous, OptedIn) 
                values (@UserId, @Username, convert(binary, @UserPassword), @IsAnonymous, @OptedIn)";

                await db.ExecuteAsync(sql, new { dataModel.UserId, dataModel.Username, dataModel.UserPassword, dataModel.IsAnonymous, dataModel.OptedIn, });
            }
        }
    }
}

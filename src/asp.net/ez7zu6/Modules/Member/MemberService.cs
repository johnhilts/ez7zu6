using System.Threading.Tasks;

namespace Member
{
    public class MemberService
    {
        public async Task<bool> CanAuthenticateUser(string username, string password)
        {
            var model = await (new AccountRepository()).GetAccountInfoByLoginPasswordAsync(username, password);
            return !model.NoMatch;
        }
    }
}

using Infrastructure;
using System.Threading.Tasks;

namespace Member
{
    public class MemberService
    {
        private readonly Settings _settings;

        public MemberService(Settings settings)
        {
            _settings = settings;
        }

        public async Task<bool> CanAuthenticateUser(string username, string password)
        {
            var model = await (new AccountRepository(_settings)).GetAccountInfoByLoginPasswordAsync(username, password);
            return !model.NoMatch;
        }
    }
}

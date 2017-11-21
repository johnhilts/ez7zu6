using System.Threading.Tasks;
using ez7zu6.Core;
using ez7zu6.Data.Models.Experience;
using ez7zu6.Data.Repositories;
using ez7zu6.Member.Models;

namespace ez7zu6.Member.Services
{
    public class MemberService
    {
        private readonly IAppEnvironment _appEnvironment;

        public MemberService(IAppEnvironment appEnvironment) => _appEnvironment = appEnvironment;

        public async Task<bool> CanAuthenticateUser(string username, string password)
        {
            var model = await (new AccountRepository(_appEnvironment)).GetAccountInfoByLoginPasswordAsync(username, password);
            return !model.NoMatch;
        }

        public async Task<int> SaveExperience(ExperienceSaveModel model)
        {
            var dataModel = new ExperienceUpdaeDataModel
            {
                UserId = model.UserId,
                Notes = model.Notes,
                InputDateTime = model.InputDateTime,
            };
            return await (new ExperienceRepository(_appEnvironment)).AddExperience(dataModel);
        }
    }
}

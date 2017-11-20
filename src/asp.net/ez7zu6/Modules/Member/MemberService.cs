using Core;
using System.Threading.Tasks;

namespace Member
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

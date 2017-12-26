using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Account;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;
using ez7zu6.Web.Models.Account;

namespace ez7zu6.Web.Services
{
    public class PresentationService
    {
        private readonly HttpContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppEnvironment _appEnvironment;

        public PresentationService(HttpContext context, IMemoryCache memoryCache, IAppEnvironment appEnvironment)
        {
            _context = context;
            _memoryCache = memoryCache;
            _appEnvironment = appEnvironment;
        }

        public async Task<UserInfoModel> CreateNewAuthenticatedSession(LoginViewModel model)
        {
            return await (new SessionService(_context, _memoryCache, _appEnvironment)).CreateNewAuthenticatedSession(model.Username, model.Password);
        }

        public UserSession GetOrCreateUserSession()
        {
            return new SessionService(_context, _memoryCache, _appEnvironment).GetOrCreateNewSession();
        }

        public async Task RemoveSession()
        {
            await new SessionService(_context, _memoryCache, _appEnvironment).RemoveSession();
        }

        public async Task<UserInfoModel> RegisterUserAndCreateNewAuthenticatedSession(RegisterViewModel model)
        {
            var validationResult = ValidateRegistration(model);
            if (!validationResult.IsValid)
                return new UserInfoModel { CanRegister = false, Message = validationResult.Message };

            var registerModel = new RegisterModel { Name = model.Name, Username = model.Username, Password = model.Password, };
            var createdModel = await (new MemberService(_appEnvironment)).Register(registerModel);
            return await (new SessionService(_context, _memoryCache, _appEnvironment)).CreateNewAuthenticatedSession(model.Username, model.Password);
        }

        private (bool IsValid, string Message) ValidateRegistration(RegisterViewModel model)
        {
            var passwordConfirmInputOk = (model.Password == model.ConfirmPassword);
            if (!passwordConfirmInputOk)
                return (false, "Password and Confirm Password do not match.");

            return (true, null);
        }
    }
}

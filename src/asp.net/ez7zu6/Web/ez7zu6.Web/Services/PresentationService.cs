using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ez7zu6.Infrastructure.Account;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;
using ez7zu6.Web.Models.Account;

namespace ez7zu6.Web.Services
{
    public class PresentationService
    {
        private readonly HttpContext _context;
        private readonly IApplicationService _applicationService;

        public PresentationService(HttpContext context, IApplicationService applicationService)
        {
            _context = context;
            _applicationService = applicationService;
        }

        public async Task<UserInfoModel> CreateNewAuthenticatedSession(LoginViewModel model)
        {
            return await (new SessionService(_context, _applicationService)).CreateNewAuthenticatedSession(model.Username, model.Password);
        }

        public UserSession GetOrCreateUserSession()
        {
            return new SessionService(_context, _applicationService).GetOrCreateNewSession();
        }

        public async Task RemoveSession()
        {
            await new SessionService(_context, _applicationService).RemoveSession();
        }

        public async Task<UserInfoModel> RegisterUserAndCreateNewAuthenticatedSession(RegisterViewModel model)
        {
            var validationResult = ValidateRegistration(model);
            if (!validationResult.IsValid)
                return new UserInfoModel { CanRegister = false, Message = validationResult.Message };

            var registerModel = new RegisterModel { Name = model.Name, Username = model.Username, Password = model.Password, };
            var userId = await (new MemberService(_applicationService.ApplicationSettings)).Register(registerModel);
            return await (new SessionService(_context, _applicationService)).CreateNewAuthenticatedSession(userId, model.Username);
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

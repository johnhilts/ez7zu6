using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Account;
using ez7zu6.Member.Models;
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
    }
}

using ez7zu6.Infrastructure.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace ez7zu6.Web.Services
{
    public class PresentationService
    {
        private readonly HttpContext _context;
        private readonly IMemoryCache _memoryCache;

        public PresentationService(HttpContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public UserSession GetOrCreateUserSession()
        {
            return new SessionService(_context, _memoryCache).GetOrCreateNewSession();
        }

    }
}

using ez7zu6.Infrastructure.Account;
using Microsoft.AspNetCore.Http;
using System;

namespace ez7zu6.Web.Services
{
    public class PresentationService
    {
        private readonly HttpContext _context;

        public PresentationService(HttpContext context)
        {
            _context = context;
        }

        public bool IsAnonymousSession()
        {
            return true;
        }

        public UserSession GetOrCreateUserSession(Guid? userId)
        {
            if (userId.HasValue)
            {
                return new SessionService(_context).GetSessionByUserId(userId.Value);
            }
            else
            {
                return new SessionService(_context).GetOrCreateNewSession(true, userId);
            }
        }
    }
}

using ez7zu6.Infrastructure.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                // get existing user session
                return null;
            }
            else
            {
                return new SessionService(_context).CreateNewSession();
            }
        }
    }
}

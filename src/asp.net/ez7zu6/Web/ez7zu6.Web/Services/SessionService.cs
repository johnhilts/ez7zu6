using ez7zu6.Infrastructure.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ez7zu6.Web.Services
{
    public class SessionService
    {
        public SessionService(HttpContext context)
        {
        }

        public UserSession CreateNewSession()
        {
            // create cookie
            return new UserSession { UserId = Guid.NewGuid(), SessionId = Guid.NewGuid(), };
        }

        public void RemoveSession(Guid sessionId)
        {
        }
    }
}

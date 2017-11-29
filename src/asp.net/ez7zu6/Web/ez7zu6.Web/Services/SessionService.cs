using System;
using Microsoft.AspNetCore.Http;
using ez7zu6.Infrastructure.Account;

namespace ez7zu6.Web.Services
{
    public class SessionService
    {
        private readonly HttpContext _context;

        public SessionService(HttpContext context)
        {
            _context = context;
        }

        public UserSession CreateNewSession(bool isAnonymous, Guid? userId)
        {
            var userSession = new UserSession { UserId = userId ?? Guid.NewGuid(), SessionId = Guid.NewGuid(), IsAnonymous = isAnonymous, };
            CreateCookie(userSession);
            // add to database
            return userSession;
        }
    
        private void CreateCookie(UserSession userSession)
        {
            var cookieDefaultExpiration = DateTime.Now.AddYears(50);
            var options = new CookieOptions { Expires = cookieDefaultExpiration, };

            _context.Response.Cookies.Append("UserSession", userSession.SessionId.ToString(), options);
            _context.Response.Cookies.Append("IsAnonymous", userSession.IsAnonymous.ToString(), options);
        }

        public void RemoveSession(Guid sessionId)
        {
        }
    }
}

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

        public UserSession GetOrCreateNewSession(bool isAnonymous, Guid? userId)
        {
            var userSession = GetExistingSession() ?? CreateNewSession(isAnonymous, userId);
            return userSession;
        }

        private UserSession CreateNewSession(bool isAnonymous, Guid? userId)
        {
            var userSession = new UserSession { UserId = userId ?? Guid.NewGuid(), SessionId = Guid.NewGuid(), IsAnonymous = isAnonymous, };
            CreateCookie(userSession);
            // add to database

            return userSession;
        }

        private UserSession GetExistingSession()
        {
            var isAnonymous = false;
            var cookieSessionExists =
                // TODO: add constants for cookie keys
                Guid.TryParse(_context.Request.Cookies["UserSession"]?.ToString() ?? string.Empty, out Guid userSessionId)
                &&
                bool.TryParse(_context.Request.Cookies["IsAnonymous"]?.ToString() ?? string.Empty, out isAnonymous);
            if (cookieSessionExists)
            {
                // get user ID from DB or Cache - I guess use IMemoryCache?
                return new UserSession { SessionId = userSessionId, IsAnonymous = isAnonymous };
            }
            else
                return null;
        }

        public UserSession GetSessionByUserId(Guid userId)
        {
            var existingSession = GetExistingSession();
            if (existingSession?.UserId == userId)
                return existingSession;
            else
            {
                // get from DB - doesn't exist in cache, so need to add it
                return null;
            }
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

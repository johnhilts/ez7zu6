using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Infrastructure.Account;

namespace ez7zu6.Web.Services
{
    public class SessionService
    {
        private readonly HttpContext _context;
        private readonly IMemoryCache _memoryCache;

        public SessionService(HttpContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public UserSession GetOrCreateNewSession()
        {
            var userSession = GetExistingSession() ?? CreateNewAnonymousSession();
            return userSession;
        }

        // TODO: we have to be able to create new non-anonymous sessions too - we'll do that when we login
        private UserSession CreateNewAnonymousSession()
        {
            var userSession = new UserSession { UserId = Guid.NewGuid(), SessionId = Guid.NewGuid(), IsAnonymous = true, };
            CreateSessionCookie(userSession);
            AddSessionToCache(userSession);
            // add to database

            return userSession;
        }

        private void AddSessionToCache(UserSession userSession)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromMinutes(20));
            _memoryCache.Set(userSession.SessionId, userSession.UserId, cacheEntryOptions);
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
                return GetExistingSession(userSessionId, isAnonymous);
            }
            else
                return null;
        }

        private UserSession GetExistingSession(Guid userSessionId, bool isAnonymous)
        {
            if (_memoryCache.TryGetValue(userSessionId, out Guid userId))
                return new UserSession { UserId = userId, SessionId = userSessionId, IsAnonymous = isAnonymous };
            else
                return null; // get from database
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

        private void CreateSessionCookie(UserSession userSession)
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

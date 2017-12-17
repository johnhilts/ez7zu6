using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Account;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;

namespace ez7zu6.Web.Services
{
    public class SessionService
    {
        private readonly HttpContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppEnvironment _appEnvironment;

        public SessionService(HttpContext context, IMemoryCache memoryCache, IAppEnvironment appEnvironment)
        {
            _context = context;
            _memoryCache = memoryCache;
            _appEnvironment = appEnvironment;
        }

        public async Task<UserInfoModel> CreateNewAuthenticatedSession(string username, string password)
        {
            var userInfo = await (new MemberService(_appEnvironment)).GetUserInfoByUsernameAndPassword(username, password);
            if (!userInfo.CanAuthenticate) return userInfo;

            var identity = new ClaimsIdentity(LoadClaims(userInfo), "login");
            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return userInfo;
        }

        private Claim[] LoadClaims(UserInfoModel model)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, model.Username),  new Claim(ClaimTypes.PrimarySid, model.UserId.ToString()), };
            return claims;
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

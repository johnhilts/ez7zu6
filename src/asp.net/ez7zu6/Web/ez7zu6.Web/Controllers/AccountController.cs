using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Web.Models.Account;
using ez7zu6.Web.Services;

namespace ez7zu6.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly string _defaultUrl = @"\profile";

        public AccountController(IOptions<SiteSettings> siteSettings, IAppEnvironment appEnvironment, IMemoryCache memoryCache)
            : base(siteSettings, appEnvironment, memoryCache) { }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? _defaultUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View();

            var userInfo = await PresentationService.CreateNewAuthenticatedSession(model);
            if (!userInfo.CanAuthenticate)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? _defaultUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View();

            var userInfo = await PresentationService.RegisterUserAndCreateNewAuthenticatedSession(model);
            if (!userInfo.CanRegister)
            {
                ModelState.AddModelError(string.Empty, $"Unable to Register User - {userInfo.Message}");
                return View();
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await PresentationService.RemoveSession();
            return Redirect("/");
        }
    }
}
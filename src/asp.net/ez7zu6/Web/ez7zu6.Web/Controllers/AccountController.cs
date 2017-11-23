using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ez7zu6.Web.Models.Account;
using ez7zu6.Core;
using ez7zu6.Member.Services;
using ez7zu6.Member.Models;

namespace ez7zu6.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _defaultUrl = @"\profile";
        private readonly IAppEnvironment _appEnvironment;

        public AccountController(IAppEnvironment appEnvironment) => _appEnvironment = appEnvironment;

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

            var userInfo = await (new MemberService(_appEnvironment)).GetUserInfoByUsernameAndPassword(model.Username, model.Password);
            if (!userInfo.CanAuthenticate)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var identity = new ClaimsIdentity(LoadClaims(userInfo), "login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Redirect(returnUrl);
        }

        private Claim[] LoadClaims(UserInfoModel model)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, model.Username),  new Claim(ClaimTypes.PrimarySid, model.UserId.ToString()), };
            return claims;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using ez7zu6.Web.Models.Account;
using Infrastructure;
using Member;

namespace ez7zu6.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly Settings _settings;
        private readonly string EnvironmentPath = "env";

        private readonly string _defaultUrl = @"\profile";

        public AccountController(IHostingEnvironment hostingEnvironment)
        {
            var configurationPath = Path.Combine(hostingEnvironment.WebRootPath ?? string.Empty, EnvironmentPath, "ConnectionString.txt");
            _settings = new Settings(null, configurationPath);
        }

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

            var canAuthenticate = await (new MemberService(_settings)).CanAuthenticateUser(model.Username, model.Password);
            if (!canAuthenticate)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var identity = new ClaimsIdentity(LoadClaims(model), "login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Redirect(returnUrl);
        }

        private Claim[] LoadClaims(LoginViewModel model)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, model.Username), };
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
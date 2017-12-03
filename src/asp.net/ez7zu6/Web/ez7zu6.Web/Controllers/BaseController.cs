using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ez7zu6.Web.Services;
using ez7zu6.Core;

namespace ez7zu6.Web.Controllers
{
    public class BaseController : Controller
    {
        private PresentationService _presentationService;
        protected PresentationService PresentationService => _presentationService ?? (_presentationService = new PresentationService(HttpContext));

        protected readonly IOptions<SiteSettings> _siteSettings;
        protected readonly IAppEnvironment _appEnvironment;

        protected BaseController(IOptions<SiteSettings> siteSettings, IAppEnvironment appEnvironment)
        {
            _siteSettings = siteSettings;
            _appEnvironment = appEnvironment;
        }
    }
}

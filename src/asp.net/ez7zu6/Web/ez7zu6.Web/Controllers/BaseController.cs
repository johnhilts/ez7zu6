using Microsoft.AspNetCore.Mvc;
using ez7zu6.Web.Services;

namespace ez7zu6.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApplicationService _applicationService;

        // NOTE: I can't pass HttpContext to the PresentationService from the CTOR because it isn't ready yet, so doing it as part of the PresentationService's lazy load
        private PresentationService _presentationService;
        protected PresentationService PresentationService => _presentationService ?? (_presentationService = new PresentationService(HttpContext, _applicationService));

        protected BaseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
    }
}

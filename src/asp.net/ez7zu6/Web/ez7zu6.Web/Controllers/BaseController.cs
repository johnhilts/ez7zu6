using Microsoft.AspNetCore.Mvc;
using ez7zu6.Web.Services;

namespace ez7zu6.Web.Controllers
{
    public class BaseController : Controller
    {
        private PresentationService _presentationService;
        protected PresentationService PresentationService => _presentationService ?? (_presentationService = new PresentationService(HttpContext));
    }
}

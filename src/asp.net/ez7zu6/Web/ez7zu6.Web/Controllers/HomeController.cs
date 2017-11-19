using Microsoft.AspNetCore.Mvc;

namespace ez7zu6.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
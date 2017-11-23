using ez7zu6.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ez7zu6.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly PresentationService _presentationService;

        public BaseController()
        {
            _presentationService = new PresentationService(HttpContext);
        }
    }
}

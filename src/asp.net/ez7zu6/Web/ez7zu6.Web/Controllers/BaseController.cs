﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Web.Services;

namespace ez7zu6.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        private PresentationService _presentationService;
        protected PresentationService PresentationService => _presentationService ?? (_presentationService = new PresentationService(HttpContext, _applicationService));

        protected readonly IOptions<SiteSettings> _siteSettings;
        protected readonly IAppEnvironment _appEnvironment;
        protected readonly IApplicationService _applicationService;

        protected BaseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
            _siteSettings = _applicationService.SiteSettings;
            _appEnvironment = _applicationService.AppEnvironment;
            _memoryCache = _applicationService.MemoryCache;
        }
    }
}

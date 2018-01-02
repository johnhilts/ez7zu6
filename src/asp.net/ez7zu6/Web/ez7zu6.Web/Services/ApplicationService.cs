using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Settings;

namespace ez7zu6.Web.Services
{
    public class ApplicationService : IApplicationService
    {
        public SiteSettings SiteSettings { get; }
        public IAppEnvironment AppEnvironment { get; }
        public IMemoryCache MemoryCache { get; }
        public ApplicationSettings ApplicationSettings { get; }

        public ApplicationService(IOptions<SiteSettings> siteSettings, IAppEnvironment appEnvironment, IMemoryCache memoryCache)
        {
            SiteSettings = siteSettings.Value;
            AppEnvironment = appEnvironment;
            MemoryCache = memoryCache;
            ApplicationSettings = new ApplicationSettings { AppEnvironment = appEnvironment, DatabaseSettings = SiteSettings.DatabaseSettings };
        }
    }
}

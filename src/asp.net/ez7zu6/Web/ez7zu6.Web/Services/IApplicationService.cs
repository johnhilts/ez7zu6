using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Settings;

namespace ez7zu6.Web.Services
{
    public interface IApplicationService
    {
        IAppEnvironment AppEnvironment { get; }
        IMemoryCache MemoryCache { get; }
        SiteSettings SiteSettings { get; }
        ApplicationSettings ApplicationSettings { get; }
    }
}
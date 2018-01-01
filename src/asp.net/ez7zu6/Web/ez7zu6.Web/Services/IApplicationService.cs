using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ez7zu6.Core;

namespace ez7zu6.Web.Services
{
    public interface IApplicationService
    {
        IAppEnvironment AppEnvironment { get; }
        IMemoryCache MemoryCache { get; }
        IOptions<SiteSettings> SiteSettings { get; }
    }
}
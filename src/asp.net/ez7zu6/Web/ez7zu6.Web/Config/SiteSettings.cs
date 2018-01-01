using ez7zu6.Infrastructure.Settings;

namespace ez7zu6.Web
{
    public class SiteSettings
    {
        public string Domain { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
    }
}
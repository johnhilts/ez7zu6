using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ez7zu6.Core;
using ez7zu6.Infrastructure.Environment;
using ez7zu6.Web.Helper;
using ez7zu6.Web.Services;

namespace ez7zu6.Web
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppEnvironment>(s => GetAppEnvironment());

            services.AddTransient<IApplicationService, ApplicationService>();

            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));

            services.AddMemoryCache();

            services.AddMvc();

            // enable cookie-based auth - see here: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // TODO: really want to make this always"
                //options.Cookie.Domain = "jfh-auth";
            });
        }

        private IAppEnvironment GetAppEnvironment()
        {
            var isAppHarbor = (new EnvironmentHelper()).IsAppHarbor;
            if (isAppHarbor)
                return new AppHarborAppEnvironment();
            else
                return new LocalAppEnvironment(HostingEnvironment.WebRootPath);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //}

            // for cookie-based auth
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}

﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace ez7zu6.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // enable cookie-based auth - see here: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // TODO: really want to make this always"
                //options.Cookie.Domain = "jfh-auth";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            void MapVirtualPath(string path) // NOTE: only works 1 level deep
            {
                var fullPath = Path.Combine(env.WebRootPath, path);

                if (!Directory.Exists(fullPath)) return;

                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(fullPath),
                    RequestPath = new PathString("/${path}"),
                    EnableDirectoryBrowsing = false
                });
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // for cookie-based auth
            app.UseAuthentication();

            MapVirtualPath("env");

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

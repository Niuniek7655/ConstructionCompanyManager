using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CCP.Application;
using CCP.Application.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CCM.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //private string cookie = "CookieAuth";        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services
                .AddIdentity<IdentityUser, IdentityRole>(ConfigIdentity)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //services
            //    .AddAuthentication(cookie)
            //    .AddCookie(cookie, ConfigCookieAuthentication);
            services.ConfigureApplicationCookie(ConfigCookieAuthentication);
            services.AddAuthorization(ConfigAuthorization);
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        private void ConfigIdentity(IdentityOptions options)
        {
            options.Password.RequiredLength = 4;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }

        private string userCookie = "UserCookie";
        private string authenticationPath = "/Home/Login";
        private void ConfigCookieAuthentication(CookieAuthenticationOptions config)
        {
            config.Cookie.Name = userCookie;
            config.LoginPath = authenticationPath;
        }

        private void ConfigAuthorization(AuthorizationOptions options)
        {
            AuthorizationPolicyBuilder authorizationBuilder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy authorizationPolicy = authorizationBuilder
                                                                    .RequireAuthenticatedUser()
                                                                    .RequireClaim(ClaimTypes.Name)
                                                                    .Build();
            options.DefaultPolicy = authorizationPolicy;
        }

        private string errorPath = "/Home/Error";
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorPath);
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(ConfigureEndPointsRoute);
        }

        private void ConfigureEndPointsRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        }
    }
}

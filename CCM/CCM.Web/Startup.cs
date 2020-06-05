using CCP.Application.Contexts;
using CCP.Infrastructure.Configuation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        private readonly AAConfiguration aAConfiguration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            aAConfiguration = new AAConfiguration();
        }
    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services
                .AddIdentity<IdentityUser, IdentityRole>(aAConfiguration.ConfigIdentity)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(aAConfiguration.ConfigCookieAuthentication);
            services.AddAuthorization(aAConfiguration.ConfigAuthorization);
            services.AddControllersWithViews();
            services.AddRazorPages();
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

        private string defaultName = "default";
        private string pattern = "{controller=Home}/{action=Index}/{id?}";
        private void ConfigureEndPointsRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: defaultName,
                    pattern: pattern);
            endpoints.MapRazorPages();
        }
    }
}

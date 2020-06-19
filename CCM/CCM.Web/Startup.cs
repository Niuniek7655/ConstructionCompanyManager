using AutoMapper;
using CCM.Domain;
using CCM.Domain.Tools;
using CCM.Model;
using CCM.Model.Tools;
using CCM.Web.Models;
using CCP.Infrastructure.Configuation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        private readonly AntiforgeryConfiguration antiforgeryConfiguration;
        private readonly HttpClientsConfiguration httpClientsConfiguration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            aAConfiguration = new AAConfiguration();
            antiforgeryConfiguration = new AntiforgeryConfiguration();
            httpClientsConfiguration = new HttpClientsConfiguration();
        }

        private const string accessClient = "AccessClient";
        public void ConfigureServices(IServiceCollection services)
        {
            BasicConfig(services);
            DIConfig(services);
        }

        private void BasicConfig(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(aAConfiguration.ConfigCookieAuthentication);
            services.AddControllersWithViews();
            services.AddRazorPages();
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddMvc(antiforgeryConfiguration.ConfigMvcOptions);
            services.AddAntiforgery(antiforgeryConfiguration.ConfigAntyforgery);
            services.AddHttpClient(accessClient, httpClientsConfiguration.ConfigAccessHttpClient);
            services.AddAutoMapper(typeof(AutoMappingConfiguration));
        }

        private void DIConfig(IServiceCollection services)
        {
            services.AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
            services.AddTransient<IBasicAccessSender, BasicAccessSender>();
        }

        private string errorPath = "/Access/Error";
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
            app.UseEndpoints(ConfigureApplicationEndpointsRoute);
        }

        private string defaultName = "default";
        private string pattern = "{controller=Access}/{action=Start}/{id?}";
        private void ConfigureApplicationEndpointsRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: defaultName,
                    pattern: pattern);
            endpoints.MapRazorPages();
        }
    }
}

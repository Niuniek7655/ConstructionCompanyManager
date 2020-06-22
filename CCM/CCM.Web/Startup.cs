using AutoMapper;
using CCM.Constants;
using CCM.Domain;
using CCM.Domain.Tools;
using CCM.Model;
using CCM.Model.Tools;
using CCM.Web.Models;
using CCP.Infrastructure.Configuations;
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
            services.AddHttpClient(ConstantValues.AccessClientName, httpClientsConfiguration.ConfigAccessHttpClient);
            services.AddAutoMapper(typeof(AutoMappingConfiguration));
            ConfigSettings(services);
        }

        private void ConfigSettings(IServiceCollection services)
        {
            IConfiguration httpRequestBuilderDataConfiguration = Configuration.GetSection(ConstantValues.HttpRequestBuilderDataSection);
            services.Configure<HttpRequestBuilderData>(httpRequestBuilderDataConfiguration);
            IConfiguration requestBodyDeserializerDataConfiguration = Configuration.GetSection(ConstantValues.RequestBodyDeserializerDataSection);
            services.Configure<RequestBodyDeserializerData>(requestBodyDeserializerDataConfiguration);
            IConfiguration basicAccessSenderDataConfiguration = Configuration.GetSection(ConstantValues.BasicAccessSenderDataSection);
            services.Configure<BasicAccessSenderData>(basicAccessSenderDataConfiguration);
        }

        private void DIConfig(IServiceCollection services)
        {
            services.AddTransient<IHttpRequestBuilder, HttpRequestBuilder>();
            services.AddTransient<IBasicAccessSender, BasicAccessSender>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ConstantValues.ErrorPath);
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(ConfigureApplicationEndpointsRoute);
        }

        private void ConfigureApplicationEndpointsRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: ConstantValues.DefaultRouteName,
                    pattern: ConstantValues.DefaultRoutePattern);
            endpoints.MapRazorPages();
        }
    }
}

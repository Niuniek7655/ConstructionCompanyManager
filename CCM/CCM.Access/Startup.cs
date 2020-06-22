using CCP.Application.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CCM.Domain.Tools;
using CCM.Model.Tools;
using CCM.Constants;
using CCP.Infrastructure.Configuations;

namespace CCM.Access
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
            BasicConfig(services);
            DIConfig(services);
        }

        private void BasicConfig(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services
                .AddIdentity<IdentityUser, IdentityRole>(aAConfiguration.ConfigIdentity)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(aAConfiguration.ConfigAuthorization);
            services.AddControllers();
            services.AddRazorPages();
            services.Configure<IISServerOptions>(ConfigIISServerOptions);
            IConfiguration accessMessageConfiguration = Configuration.GetSection(ConstantValues.AccessMessageSection);
            services.Configure<AccessMessage>(accessMessageConfiguration);
        }

        private void ConfigIISServerOptions(IISServerOptions options)
        {
            options.AllowSynchronousIO = true;
        }

        private void DIConfig(IServiceCollection services)
        {
            services.AddSingleton<IRequestBodyDeserializer, RequestBodyDeserializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(ConfigureAccessAPIEndpointsRoute);
            app.UseAuthorization();
            app.UseMiddleware<RRMiddleware>();
        }

        private void ConfigureAccessAPIEndpointsRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        }
    }
}

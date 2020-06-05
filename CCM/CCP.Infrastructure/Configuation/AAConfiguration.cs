using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CCP.Infrastructure.Configuation
{
    public class AAConfiguration
    {
        public void ConfigIdentity(IdentityOptions options)
        {
            options.Password.RequiredLength = 4;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }

        private string userCookie = "UserCookie";
        private string authenticationPath = "/Home/Login";
        public void ConfigCookieAuthentication(CookieAuthenticationOptions config)
        {
            config.Cookie.Name = userCookie;
            config.LoginPath = authenticationPath;
        }

        public void ConfigAuthorization(AuthorizationOptions options)
        {
            AuthorizationPolicyBuilder authorizationBuilder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy authorizationPolicy = authorizationBuilder
                                                                    .RequireAuthenticatedUser()
                                                                    .RequireClaim(ClaimTypes.Name)
                                                                    .Build();
            options.DefaultPolicy = authorizationPolicy;
        }
    }
}

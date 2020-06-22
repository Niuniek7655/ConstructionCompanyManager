using CCM.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CCP.Infrastructure.Configuations
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

        public void ConfigCookieAuthentication(CookieAuthenticationOptions config)
        {
            config.Cookie.Name = ConstantValues.UserCookieName;
            config.LoginPath = ConstantValues.AuthenticationPath;
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

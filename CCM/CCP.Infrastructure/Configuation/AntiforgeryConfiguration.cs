using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace CCP.Infrastructure.Configuation
{
    public class AntiforgeryConfiguration
    {
        private string antiforgeryHeaderName = "__RequestVerificationToken";
        public void ConfigAntyforgery(AntiforgeryOptions option)
        {
            option.HeaderName = antiforgeryHeaderName;
        }

        public void ConfigMvcOptions(MvcOptions options)
        {
            AutoValidateAntiforgeryTokenAttribute token = new AutoValidateAntiforgeryTokenAttribute();
            options.Filters.Add(token);
        }
    }
}

using CCM.Constants;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace CCP.Infrastructure.Configuations
{
    public class AntiforgeryConfiguration
    {
        public void ConfigAntyforgery(AntiforgeryOptions option)
        {
            option.HeaderName = ConstantValues.AntiforgeryHeaderName;
        }

        public void ConfigMvcOptions(MvcOptions options)
        {
            AutoValidateAntiforgeryTokenAttribute token = new AutoValidateAntiforgeryTokenAttribute();
            options.Filters.Add(token);
        }
    }
}

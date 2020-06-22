using System.Threading.Tasks;
using CCM.Constants;
using CCM.Domain;
using CCM.Domain.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CCM.Access.Controllers
{
    [Route(ConstantValues.ControllerActionRoute)]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly ILogger<BasicController> _logger;
        private readonly AccessMessage _accessMessage;
        private readonly IRequestBodyDeserializer _requestDeserializer;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public BasicController(ILogger<BasicController> logger, IOptions<AccessMessage> settings, IRequestBodyDeserializer requestDeserializer, 
                               UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _accessMessage = settings.Value;
            _requestDeserializer = requestDeserializer;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            ILoginData model = _requestDeserializer.DeserializerRequest<ILoginData>(Request.Body);
            IdentityUser user = await _userManager.FindByNameAsync(model.Login);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation(_accessMessage.Ms1, model.Login);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation(_accessMessage.Ms2, model.Login);
                    return Unauthorized();
                }
            }
            else
            {
                _logger.LogInformation(_accessMessage.Ms3, model.Login);
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            IRegisterData model = _requestDeserializer.DeserializerRequest<IRegisterData>(Request.Body);
            IdentityUser user = new IdentityUser();
            user.UserName = model.Login;
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(_accessMessage.Ms4, model.Login);
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation(_accessMessage.Ms1, model.Login);
                    return Ok();

                }
                else
                {
                    _logger.LogInformation(_accessMessage.Ms2, model.Login);
                    return Unauthorized();
                }
            }
            else
            {
                _logger.LogInformation(_accessMessage.Ms5, model.Login);
                return Forbid();
            }
        }
    }
}

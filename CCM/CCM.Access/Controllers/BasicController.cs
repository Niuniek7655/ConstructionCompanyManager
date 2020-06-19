using System.Threading.Tasks;
using CCM.Domain;
using CCM.Domain.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CCM.Access.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BasicController : ControllerBase 
    {
        private readonly ILogger<BasicController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRequestBodyDeserializer _requestDeserializer;

        public BasicController(ILogger<BasicController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IRequestBodyDeserializer requestDeserializer)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _requestDeserializer = requestDeserializer;
        }

        private const string ms1 = "User login correct to application";
        private const string ms2 = "User can not login to application";
        private const string ms3 = "Can not find user";
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
                    _logger.LogInformation(ms1, model.Login);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation(ms2, model.Login);
                    return Unauthorized();
                }
            }
            else
            {
                _logger.LogInformation(ms3, model.Login);
                return NotFound();
            }
        }

        private const string ms4 = "User account create correct";
        private const string ms5 = "Can not register user correct";
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            IRegisterData model = _requestDeserializer.DeserializerRequest<IRegisterData>(Request.Body);
            IdentityUser user = new IdentityUser();
            user.UserName = model.Login;
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(ms4, model.Login);
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation(ms1, model.Login);
                    return Ok();
                    
                }
                else
                {
                    _logger.LogInformation(ms2, model.Login);
                    return Unauthorized();
                }
            }
            else
            {
                _logger.LogInformation(ms5, model.Login);
                return Forbid();
            }
        }
    }
}

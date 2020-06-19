using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CCM.Web.Models;
using AutoMapper;
using CCM.Model;
using CCM.Domain;
using CCM.Domain.Enums;

namespace CCM.Web.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        private readonly IMapper _mapper;
        private readonly IBasicAccessSender _basicAccessSender;

        public AccessController(ILogger<AccessController> logger, IMapper mapper, IBasicAccessSender basicAccessSender)
        {
            _logger = logger;
            _mapper = mapper;
            _basicAccessSender = basicAccessSender;
        }

        public IActionResult Start()
        {        
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            ILoginData model = _mapper.Map<LoginViewModel, LoginData>(viewModel);
            LoginStatus isLogin = await _basicAccessSender.Login(model);
            RedirectToActionResult result = null;
            switch (isLogin)
            {
                case LoginStatus.LoginCorrect:
                    result = RedirectToAction("Start");
                    break;
                case LoginStatus.CanNotLoginToAccount:
                    result = RedirectToAction("Start");
                    break;
                case LoginStatus.UserNotFound:
                    result = RedirectToAction("Start");
                    break;
                default:
                    result = RedirectToAction("Start");
                    break;
            }
            return result;
        }

        public IActionResult LoginOnFb()
        {
            return RedirectToAction("Start");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            IRegisterData model = _mapper.Map<RegisterViewModel, RegisterData>(viewModel);
            RegistrationStatus isRegisterAndLogin = await _basicAccessSender.Register(model);
            RedirectToActionResult result = null;
            switch (isRegisterAndLogin)
            {
                case RegistrationStatus.AccountCreated:
                    result = RedirectToAction("Start");
                    break;
                case RegistrationStatus.AccountCreatedTryLoginAgain:
                    result = RedirectToAction("Start");
                    break;
                case RegistrationStatus.CanNotCreateAccount:
                    result = RedirectToAction("Start");
                    break;
                default:
                    result = RedirectToAction("Start");
                    break;
            }
            return result;
        }

        [HttpPost]
        public IActionResult RegisterOnFb()
        {
            return RedirectToAction("Start");
        }

        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Start");
        }

        public async Task<IActionResult> LogoutFromFb()
        {
            return RedirectToAction("Start");
        }
    }
}

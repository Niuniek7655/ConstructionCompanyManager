using System.Threading.Tasks;
using CCM.Access.Extensions;
using CCM.Constants;
using CCM.Domain;
using CCM.Domain.Enums;
using CCM.Domain.Loggers;
using CCM.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CCM.Access.Controllers
{
    [Route(ConstantValues.ControllerActionRoute)]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly IAccessManager _accessManager;
        private readonly IAcessAPILogger<BasicController> _logger;

        public BasicController(IAccessManager accessManager, IAcessAPILogger<BasicController> logger)
        {
            _accessManager = accessManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDataDTO model)
        {
            LoginStatus status = await _accessManager.Login(model);
            _logger.LogLoginState(status, model.Login);
            IActionResult result = status.LoginStatusResponse();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDataDTO model)
        {
            RegistrationStatus status = await _accessManager.Register(model);
            _logger.LogRegistrationState(status, model.Login);
            IActionResult result = status.RegisterStatusResponse();
            return result;
        }
    }
}

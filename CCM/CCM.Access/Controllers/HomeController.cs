using CCM.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CCM.Access.Controllers
{
    [Route(ConstantValues.ControllerRoute)]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

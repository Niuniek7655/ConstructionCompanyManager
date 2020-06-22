using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCM.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CCM.Access.Controllers
{
    [Route(ConstantValues.ControllerRoute)]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly ILogger<FacebookController> _logger;

        public FacebookController(ILogger<FacebookController> logger)
        {
            _logger = logger;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CCM.Web.Models;
using System.Security.Claims;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CCM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, 
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Password);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation("User login correct to application", model.Login);
                    RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult LoginOnFb()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerUser)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = registerUser.Login;
            IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User account create correct", registerUser.Login);
                var signInResult = await _signInManager.PasswordSignInAsync(user, registerUser.Password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation("User login correct to application", registerUser.Login);
                    RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RegisterOnFb()
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

using CCM.Domain;
using CCM.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CCP.Application
{
    public class AccessManager : IAccessManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccessManager(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginStatus> Login(ILoginData model)
        {
            LoginStatus status;
            IdentityUser user = await _userManager.FindByNameAsync(model.Login);
            if (user != null)
            {
                SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    status = LoginStatus.LoginCorrect;
                }
                else
                {
                    status = LoginStatus.CanNotLoginToAccount;
                }
            }
            else
            {
                status = LoginStatus.UserNotFound;
            }
            return status;
        }

        public async Task<RegistrationStatus> Register(IRegisterData model)
        {
            RegistrationStatus status;
            IdentityUser user = new IdentityUser();
            user.UserName = model.Login;
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    status = RegistrationStatus.AccountCreated;
                }
                else
                {
                    status = RegistrationStatus.AccountCreatedTryLoginAgain;
                }
            }
            else
            {
                status = RegistrationStatus.CanNotCreateAccount;
            }
            return status;
        }
    }
}

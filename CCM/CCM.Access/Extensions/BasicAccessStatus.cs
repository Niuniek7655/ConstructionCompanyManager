using CCM.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CCM.Access.Extensions
{
    public static class BasicAccessStatus
    {
        public static IActionResult LoginStatusResponse(this LoginStatus status)
        {
            IActionResult result = null;
            switch (status)
            {
                case LoginStatus.LoginCorrect:
                    result = new OkResult();
                    break;
                case LoginStatus.CanNotLoginToAccount:
                    result = new UnauthorizedResult();
                    break;
                case LoginStatus.UserNotFound:
                    result = new NotFoundResult();
                    break;
                case LoginStatus.UnexpectedError:
                    result = new StatusCodeResult(500);
                    break; 
            }
            return result;
        }

        public static IActionResult RegisterStatusResponse(this RegistrationStatus status)
        {
            IActionResult result = null;
            switch (status)
            {
                case RegistrationStatus.AccountCreated:
                    result = new OkResult();
                    break;
                case RegistrationStatus.AccountCreatedTryLoginAgain:
                    result = new UnauthorizedResult();
                    break;
                case RegistrationStatus.CanNotCreateAccount:
                    result = new ForbidResult();
                    break;
                case RegistrationStatus.UnexpectedError:
                    result = new StatusCodeResult(500);
                    break;
            }
            return result;
        }
    }
}

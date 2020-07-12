using CCM.Domain.Enums;
using System.Threading.Tasks;

namespace CCM.Domain.Tools
{
    public interface IHttpMessageSender
    {
        Task<LoginStatus> Login(ILoginData loginData);
        Task<RegistrationStatus> Register(IRegisterData registerData);
    }
}

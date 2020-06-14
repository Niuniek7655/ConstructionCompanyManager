using CCM.Domain.Enums;
using System.Threading.Tasks;

namespace CCM.Domain
{
    public interface IBasicAccessSender
    {
        Task<LoginStatus> Login(ILoginData loginData);
        Task<RegistrationStatus> Register(IRegisterData registerData);
    }
}

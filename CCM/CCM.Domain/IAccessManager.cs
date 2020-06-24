using CCM.Domain.Enums;
using System.Threading.Tasks;

namespace CCM.Domain
{
    public interface IAccessManager
    {
        Task<LoginStatus> Login(ILoginData model);
        Task<RegistrationStatus> Register(IRegisterData model);
    }
}

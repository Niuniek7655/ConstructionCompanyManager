using CCM.Domain;
using CCM.Domain.Tools;
using System.Threading.Tasks;
using CCM.Domain.Enums;
using CCM.Domain.Loggers;

namespace CCM.Model
{
    public class BasicAccessSender : IBasicAccessSender
    {
        private readonly IHttpMessageSender _httpMessageSender;
        private readonly IBasicAcessAPILogger<BasicAccessSender> _logger;

        public BasicAccessSender(IHttpMessageSender httpMessageSender, IBasicAcessAPILogger<BasicAccessSender> logger)
        {
            _httpMessageSender = httpMessageSender;
            _logger = logger;
        }

        public async Task<LoginStatus> Login(ILoginData loginData)
        {
            LoginStatus status = await _httpMessageSender.Login(loginData);
            _logger.LogLoginState(status, loginData.Login);
            return status;
        }

        public async Task<RegistrationStatus> Register(IRegisterData registerData)
        {
            RegistrationStatus status = await _httpMessageSender.Register(registerData);
            _logger.LogRegistrationState(status, registerData.Login);
            return status;
        }
    }
}

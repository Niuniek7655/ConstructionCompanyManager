using CCM.Constants;
using CCM.Domain.Enums;
using CCM.Domain.Loggers;
using CCP.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CCP.Application
{
    public class BasicAcessAPILogger<T> : IBasicAcessAPILogger<T> where T : class
    {
        private readonly ILogger<T> _logger;
        public BasicAcessAPILogger(ILogger<T> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            BasicAccessMessage = settings.Value.BasicAccessMessage;
            BasicAccessSenderData = settings.Value.BasicAccessSenderData;
            HttpRequestBuilderData = settings.Value.HttpRequestBuilderData;
            IncorrectEnumType = settings.Value.IncorrectEnumType;
        }

        public BasicAccessMessage BasicAccessMessage { get; private set; }
        public BasicAccessSenderData BasicAccessSenderData { get; private set; }
        public HttpRequestBuilderData HttpRequestBuilderData { get; private set; }
        public string IncorrectEnumType { get; private set; }

        public void LogLoginState(LoginStatus status, string login)
        {
            switch (status)
            {
                case LoginStatus.LoginCorrect:
                    _logger.LogInformation(BasicAccessMessage.Ms1, login);
                    break;
                case LoginStatus.CanNotLoginToAccount:
                    _logger.LogInformation(BasicAccessMessage.Ms2, login);
                    break;
                case LoginStatus.UserNotFound:
                    _logger.LogInformation(BasicAccessMessage.Ms3, login);
                    break;
                case LoginStatus.UnexpectedError:
                    _logger.LogInformation(BasicAccessMessage.Ms6, login);
                    break;
                default:
                    throw new AcessAPILoggerException(IncorrectEnumType);
            }
        }

        public void LogRegistrationState(RegistrationStatus status, string login)
        {
            switch (status)
            {
                case RegistrationStatus.AccountCreated:
                    _logger.LogInformation(BasicAccessMessage.Ms4, login);
                    _logger.LogInformation(BasicAccessMessage.Ms1, login);
                    break;
                case RegistrationStatus.AccountCreatedTryLoginAgain:
                    _logger.LogInformation(BasicAccessMessage.Ms4, login);
                    _logger.LogInformation(BasicAccessMessage.Ms2, login);
                    break;
                case RegistrationStatus.CanNotCreateAccount:
                    _logger.LogInformation(BasicAccessMessage.Ms5, login);
                    break;
                case RegistrationStatus.UnexpectedError:
                    _logger.LogInformation(BasicAccessMessage.Ms6, login);
                    break;
                default:
                    throw new AcessAPILoggerException(IncorrectEnumType);
            }
        }
    }
}

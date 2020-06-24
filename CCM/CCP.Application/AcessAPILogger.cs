using CCM.Constants;
using CCM.Domain.Enums;
using CCM.Domain.Loggers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace CCP.Application
{
    public class AcessAPILogger<T> : IAcessAPILogger<T> where T : class
    {
        private readonly ILogger<T> _logger;
        public AcessAPILogger(ILogger<T> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            AccessMessage = settings.Value.AccessMessage;
            BasicAccessSenderData = settings.Value.BasicAccessSenderData;
            HttpRequestBuilderData = settings.Value.HttpRequestBuilderData;
            IncorrectEnumType = settings.Value.IncorrectEnumType;
        }

        public AccessMessage AccessMessage { get; private set; }
        public BasicAccessSenderData BasicAccessSenderData { get; private set; }
        public HttpRequestBuilderData HttpRequestBuilderData { get; private set; }
        public string IncorrectEnumType { get; private set; }

        public void LogLoginState(LoginStatus status, string login)
        {
            switch (status)
            {
                case LoginStatus.LoginCorrect:
                    _logger.LogInformation(AccessMessage.Ms1, login);
                    break;
                case LoginStatus.CanNotLoginToAccount:
                    _logger.LogInformation(AccessMessage.Ms2, login);
                    break;
                case LoginStatus.UserNotFound:
                    _logger.LogInformation(AccessMessage.Ms3, login);
                    break;
                case LoginStatus.UnexpectedError:
                    _logger.LogInformation(BasicAccessSenderData.Ms4, login);
                    break;
                default:
                    throw new Exception(IncorrectEnumType);
            }
        }

        public void LogRegistrationState(RegistrationStatus status, string login)
        {
            switch (status)
            {
                case RegistrationStatus.AccountCreated:
                    _logger.LogInformation(AccessMessage.Ms4, login);
                    _logger.LogInformation(AccessMessage.Ms1, login);
                    break;
                case RegistrationStatus.AccountCreatedTryLoginAgain:
                    _logger.LogInformation(AccessMessage.Ms4, login);
                    _logger.LogInformation(AccessMessage.Ms2, login);
                    break;
                case RegistrationStatus.CanNotCreateAccount:
                    _logger.LogInformation(AccessMessage.Ms5, login);
                    break;
                case RegistrationStatus.UnexpectedError:
                    _logger.LogInformation(BasicAccessSenderData.Ms4, login);
                    break;
                default:
                    throw new Exception(IncorrectEnumType);
            }
        }
    }
}

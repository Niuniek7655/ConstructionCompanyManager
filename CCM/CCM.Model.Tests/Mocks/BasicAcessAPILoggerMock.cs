using CCM.Constants;
using CCM.Domain.Enums;
using CCM.Domain.Loggers;
using System;

namespace CCM.Model.Tests.Mocks
{
    public class BasicAcessAPILoggerMock<T> : IBasicAcessAPILogger<T> where T : class
    {
        public BasicAccessMessage BasicAccessMessage => throw new NotImplementedException();
        public BasicAccessSenderData BasicAccessSenderData => throw new NotImplementedException();
        public HttpRequestBuilderData HttpRequestBuilderData => throw new NotImplementedException();
        public string IncorrectEnumType => throw new NotImplementedException();

        public void LogLoginState(LoginStatus status, string login)
        {

        }

        public void LogRegistrationState(RegistrationStatus status, string login)
        {

        }
    }
}

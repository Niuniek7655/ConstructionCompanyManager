using CCM.Constants.Tests;
using CCM.Domain.Enums;
using CCM.Model.DTO;
using CCM.Model.Tests.Mocks;
using CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks;
using CCM.Model.Tools;
using System.Threading.Tasks;
using Xunit;

namespace CCM.Model.Tests
{
    public class BasicAccessSenderTests
    {
        #region Arrange method
        private HttpMessageSender CreateOKHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            OKHttpClientFactoryMock client = new OKHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private BasicAccessSender CreateOKBasicAccessSender(BasicAcessAPILoggerMock<BasicAccessSender> logger)
        {
            HttpMessageSender sender = CreateOKHttpMessageSender();
            BasicAccessSender accessSender = new BasicAccessSender(sender, logger);
            return accessSender;
        }

        private HttpMessageSender CreateUnauthorizedHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            UnauthorizedHttpClientFactoryMock client = new UnauthorizedHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private BasicAccessSender CreateUnauthorizedBasicAccessSender(BasicAcessAPILoggerMock<BasicAccessSender> logger)
        {
            HttpMessageSender sender = CreateUnauthorizedHttpMessageSender();
            BasicAccessSender accessSender = new BasicAccessSender(sender, logger);
            return accessSender;
        }

        private HttpMessageSender CreateForbiddenHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            ForbiddenHttpClientFactoryMock client = new ForbiddenHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private BasicAccessSender CreateForbiddenBasicAccessSender(BasicAcessAPILoggerMock<BasicAccessSender> logger)
        {
            HttpMessageSender sender = CreateForbiddenHttpMessageSender();
            BasicAccessSender accessSender = new BasicAccessSender(sender, logger);
            return accessSender;
        }

        private HttpMessageSender CreateNotFoundHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            NotFoundHttpClientFactoryMock client = new NotFoundHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private BasicAccessSender CreateNotFoundBasicAccessSender(BasicAcessAPILoggerMock<BasicAccessSender> logger)
        {
            HttpMessageSender sender = CreateNotFoundHttpMessageSender();
            BasicAccessSender accessSender = new BasicAccessSender(sender, logger);
            return accessSender;
        }

        private HttpMessageSender CreateOtherHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            OtherHttpClientFactoryMock client = new OtherHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private BasicAccessSender CreateOtherHttpBasicAccessSender(BasicAcessAPILoggerMock<BasicAccessSender> logger)
        {
            HttpMessageSender sender = CreateOtherHttpMessageSender();
            BasicAccessSender accessSender = new BasicAccessSender(sender, logger);
            return accessSender;
        }
        #endregion Arrange method

        #region Login
        [Fact]
        public async Task Login_UserCanLogin_ReturnLoginCorrectStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateOKBasicAccessSender(logger);
            LoginDataDTO loginData = new LoginDataDTO();

            //Act
            LoginStatus status = await sender.Login(loginData);

            //Assert
            Assert.Equal(LoginStatus.LoginCorrect, status);
        }

        [Fact]
        public async Task Login_UserCanNotLogin_ReturnCanNotLoginToAccountStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateUnauthorizedBasicAccessSender(logger);
            LoginDataDTO loginData = new LoginDataDTO();

            //Act
            LoginStatus status = await sender.Login(loginData);

            //Assert
            Assert.Equal(LoginStatus.CanNotLoginToAccount, status);
        }

        [Fact]
        public async Task Login_CanNotFindUserInApplication_ReturnUserNotFoundStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateNotFoundBasicAccessSender(logger);
            LoginDataDTO loginData = new LoginDataDTO();

            //Act
            LoginStatus status = await sender.Login(loginData);

            //Assert
            Assert.Equal(LoginStatus.UserNotFound, status);
        }

        [Fact]
        public async Task Login_ErrorOrDifferentResponseFromAccessServer_ReturnUnexpectedErrorStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateOtherHttpBasicAccessSender(logger);
            LoginDataDTO loginData = new LoginDataDTO();

            //Act
            LoginStatus status = await sender.Login(loginData);

            //Assert
            Assert.Equal(LoginStatus.UnexpectedError, status);
        }
        #endregion Login

        #region Register
        [Fact]
        public async Task Register_UserRegisterAndLoginCorrect_ReturnAccountCreatedStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateOKBasicAccessSender(logger);
            RegisterDataDTO registerData = new RegisterDataDTO();

            //Act
            RegistrationStatus status = await sender.Register(registerData);

            //Assert
            Assert.Equal(RegistrationStatus.AccountCreated, status);
        }

        [Fact]
        public async Task Register_UserRegisterCorrectButCanNotLogin_ReturnAccountCreatedTryLoginAgainStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateUnauthorizedBasicAccessSender(logger);
            RegisterDataDTO registerData = new RegisterDataDTO();

            //Act
            RegistrationStatus status = await sender.Register(registerData);

            //Assert
            Assert.Equal(RegistrationStatus.AccountCreatedTryLoginAgain, status);
        }

        [Fact]
        public async Task Register_UserCanNotCreateAccount_ReturnCanNotCreateAccountStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateForbiddenBasicAccessSender(logger);
            RegisterDataDTO registerData = new RegisterDataDTO();

            //Act
            RegistrationStatus status = await sender.Register(registerData);

            //Assert
            Assert.Equal(RegistrationStatus.CanNotCreateAccount, status);
        }

        [Fact]
        public async Task Register_ErrorOrDifferentResponseFromAccessServer_ReturnUnexpectedErrorStatus()
        {
            //Arrange
            BasicAcessAPILoggerMock<BasicAccessSender> logger = new BasicAcessAPILoggerMock<BasicAccessSender>();
            BasicAccessSender sender = CreateOtherHttpBasicAccessSender(logger);
            RegisterDataDTO registerData = new RegisterDataDTO();

            //Act
            RegistrationStatus status = await sender.Register(registerData);

            //Assert
            Assert.Equal(RegistrationStatus.UnexpectedError, status);
        }
        #endregion Register
    }
}

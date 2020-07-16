using CCM.Constants.Tests;
using CCM.Domain.Enums;
using CCM.Model.DTO;
using CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks;
using CCM.Model.Tools;
using System.Threading.Tasks;
using Xunit;

namespace CCM.Model.Tests.Tools
{
    public class HttpMessageSenderTests
    {
        #region Arrange methods
        private HttpMessageSender CreateOKHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            OKHttpClientFactoryMock client = new OKHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private HttpMessageSender CreateUnauthorizedHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            UnauthorizedHttpClientFactoryMock client = new UnauthorizedHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private HttpMessageSender CreateForbiddenHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            ForbiddenHttpClientFactoryMock client = new ForbiddenHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private HttpMessageSender CreateNotFoundHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            NotFoundHttpClientFactoryMock client = new NotFoundHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }

        private HttpMessageSender CreateOtherHttpMessageSender()
        {
            SettingsMock settings = new SettingsMock();
            OtherHttpClientFactoryMock client = new OtherHttpClientFactoryMock();
            HttpRequestBuilderMock requestBuilderMock = new HttpRequestBuilderMock();
            HttpMessageSender httpMessageSender = new HttpMessageSender(settings, client, requestBuilderMock);
            return httpMessageSender;
        }
        #endregion Arrange methods

        #region Login
        [Fact]
        public async Task Login_UserCanLogin_ReturnLoginCorrectStatus()
        {
            //Arrange
            HttpMessageSender sender = CreateOKHttpMessageSender();
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
            HttpMessageSender sender = CreateUnauthorizedHttpMessageSender();
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
            HttpMessageSender sender = CreateNotFoundHttpMessageSender();
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
            HttpMessageSender sender = CreateOtherHttpMessageSender();
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
            HttpMessageSender sender = CreateOKHttpMessageSender();
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
            HttpMessageSender sender = CreateUnauthorizedHttpMessageSender();
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
            HttpMessageSender sender = CreateForbiddenHttpMessageSender();
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
            HttpMessageSender sender = CreateOtherHttpMessageSender();
            RegisterDataDTO registerData = new RegisterDataDTO();

            //Act
            RegistrationStatus status = await sender.Register(registerData);

            //Assert
            Assert.Equal(RegistrationStatus.UnexpectedError, status);
        }
        #endregion Register
    }
}

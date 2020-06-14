using CCM.Domain;
using System.Net.Http;
using CCM.Domain.Tools;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using CCM.Domain.Enums;

namespace CCM.Model
{
    public class BasicAccessSender : IBasicAccessSender
    {
        private readonly ILogger<BasicAccessSender> _logger;
        private readonly IHttpRequestBuilder _requestBuilder;
        private readonly IHttpClientFactory _clientFactory;
        private const string accessClient = "AccessClient";
        public BasicAccessSender(ILogger<BasicAccessSender> logger, IHttpClientFactory clientFactory, IHttpRequestBuilder requestBuilder)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _requestBuilder = requestBuilder;
        }

        private const string loginUri = "Basic/Login";
        private const string ms1 = "User login correct to application";
        private const string ms2 = "User can not login to application";
        private const string ms3 = "Can not find user";
        private const string ms4 = "Unexpected error";
        public async Task<LoginStatus> Login(ILoginData loginData)
        {
            HttpRequestMessage request = _requestBuilder
                                                    .AddHttpMethod(HttpMethod.Post)
                                                    .AddUri(loginUri)
                                                    .AddContent(loginData)
                                                    .Build<HttpRequestMessage>();
            LoginStatus status;
            using(HttpClient client = _clientFactory.CreateClient(accessClient))
            {
                HttpResponseMessage response = await client.SendAsync(request);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            status = LoginStatus.LoginCorrect;
                            _logger.LogInformation(ms1, loginData.Login);
                        };
                        break;
                    case HttpStatusCode.Unauthorized:
                        {
                            status = LoginStatus.CanNotLoginToAccount;
                            _logger.LogInformation(ms2, loginData.Login);
                        };
                        break;
                    case HttpStatusCode.NotFound:
                        {
                            status = LoginStatus.UserNotFound;
                            _logger.LogInformation(ms3, loginData.Login);
                        };
                        break;
                    default:
                        {
                            status = LoginStatus.UnexpectedError;
                            _logger.LogInformation(ms4, loginData.Login);
                        };
                        break;
                }
            }
            return status;
        }

        private const string registerUri = "Basic/Register";
        private const string ms5 = "User account create correct";
        private const string ms6 = "Can not register user correct";
        public async Task<RegistrationStatus> Register(IRegisterData registerData)
        {
            HttpRequestMessage request = _requestBuilder
                                                    .AddHttpMethod(HttpMethod.Post)
                                                    .AddUri(registerUri)
                                                    .AddContent(registerData)
                                                    .Build<HttpRequestMessage>();
            RegistrationStatus status;
            using (HttpClient client = _clientFactory.CreateClient(accessClient))
            {
                HttpResponseMessage response = await client.SendAsync(request);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            status = RegistrationStatus.AccountCreated;
                            _logger.LogInformation(ms5, registerData.Login);
                            _logger.LogInformation(ms1, registerData.Login);
                        };
                        break;
                    case HttpStatusCode.Unauthorized:
                        {
                            status = RegistrationStatus.AccountCreatedTryLoginAgain;
                            _logger.LogInformation(ms5, registerData.Login);
                            _logger.LogInformation(ms2, registerData.Login);
                        };
                        break;
                    case HttpStatusCode.Forbidden:
                        {
                            status = RegistrationStatus.CanNotCreateAccount;
                            _logger.LogInformation(ms6, registerData.Login);
                        };
                        break;
                    default:
                        {
                            status = RegistrationStatus.UnexpectedError;
                            _logger.LogInformation(ms4, registerData.Login);
                        };
                        break;
                }
            }
            return status;
        }
    }
}

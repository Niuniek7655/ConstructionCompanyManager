using CCM.Domain;
using System.Net.Http;
using CCM.Domain.Tools;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using CCM.Domain.Enums;
using Microsoft.Extensions.Options;
using CCM.Constants;
using CCM.Model.DTO;

namespace CCM.Model
{
    public class BasicAccessSender : IBasicAccessSender
    {
        private readonly ILogger<BasicAccessSender> _logger;
        private readonly BasicAccessSenderData _data;
        private readonly IHttpRequestBuilder _requestBuilder;
        private readonly IHttpClientFactory _clientFactory;
        public BasicAccessSender(ILogger<BasicAccessSender> logger, IOptions<Settings> settings, IHttpClientFactory clientFactory, IHttpRequestBuilder requestBuilder)
        {
            _logger = logger;
            _data = settings.Value.BasicAccessSenderData;
            _clientFactory = clientFactory;
            _requestBuilder = requestBuilder;
        }

        public async Task<LoginStatus> Login(ILoginData loginData)
        {
            HttpRequestMessage request = _requestBuilder
                                                    .AddHttpMethod(HttpMethod.Post)
                                                    .AddUri(_data.LoginUri)
                                                    .AddContent(loginData as LoginDataDTO)
                                                    .Build<HttpRequestMessage>();
            LoginStatus status;
            using(HttpClient client = _clientFactory.CreateClient(_data.AccessClient))
            {
                HttpResponseMessage response = await client.SendAsync(request);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            status = LoginStatus.LoginCorrect;
                            _logger.LogInformation(_data.Ms1, loginData.Login);
                        };
                        break;
                    case HttpStatusCode.Unauthorized:
                        {
                            status = LoginStatus.CanNotLoginToAccount;
                            _logger.LogInformation(_data.Ms2, loginData.Login);
                        };
                        break;
                    case HttpStatusCode.NotFound:
                        {
                            status = LoginStatus.UserNotFound;
                            _logger.LogInformation(_data.Ms3, loginData.Login);
                        };
                        break;
                    default:
                        {
                            status = LoginStatus.UnexpectedError;
                            _logger.LogInformation(_data.Ms4, loginData.Login);
                        };
                        break;
                }
            }
            return status;
        }

        public async Task<RegistrationStatus> Register(IRegisterData registerData)
        {
            HttpRequestMessage request = _requestBuilder
                                                    .AddHttpMethod(HttpMethod.Post)
                                                    .AddUri(_data.RegisterUri)
                                                    .AddContent(registerData as RegisterDataDTO)
                                                    .Build<HttpRequestMessage>();
            RegistrationStatus status;
            using (HttpClient client = _clientFactory.CreateClient(_data.AccessClient))
            {
                HttpResponseMessage response = await client.SendAsync(request);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            status = RegistrationStatus.AccountCreated;
                            _logger.LogInformation(_data.Ms5, registerData.Login);
                            _logger.LogInformation(_data.Ms1, registerData.Login);
                        };
                        break;
                    case HttpStatusCode.Unauthorized:
                        {
                            status = RegistrationStatus.AccountCreatedTryLoginAgain;
                            _logger.LogInformation(_data.Ms5, registerData.Login);
                            _logger.LogInformation(_data.Ms2, registerData.Login);
                        };
                        break;
                    case HttpStatusCode.Forbidden:
                        {
                            status = RegistrationStatus.CanNotCreateAccount;
                            _logger.LogInformation(_data.Ms6, registerData.Login);
                        };
                        break;
                    default:
                        {
                            status = RegistrationStatus.UnexpectedError;
                            _logger.LogInformation(_data.Ms4, registerData.Login);
                        };
                        break;
                }
            }
            return status;
        }
    }
}

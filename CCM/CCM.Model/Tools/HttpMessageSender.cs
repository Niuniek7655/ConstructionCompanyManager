using CCM.Constants;
using CCM.Domain;
using CCM.Domain.Enums;
using CCM.Domain.Tools;
using CCM.Model.DTO;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CCM.Model.Tools
{
    public class HttpMessageSender : IHttpMessageSender
    {
        private readonly BasicAccessSenderData _data;
        private readonly IHttpRequestBuilder _requestBuilder;
        private readonly IHttpClientFactory _clientFactory;

        public HttpMessageSender(IOptions<Settings> settings, IHttpClientFactory clientFactory, IHttpRequestBuilder requestBuilder)
        {
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
            using (HttpClient client = _clientFactory.CreateClient(_data.AccessClient))
            {
                HttpResponseMessage response = await client.SendAsync(request);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        status = LoginStatus.LoginCorrect;
                        break;
                    case HttpStatusCode.Unauthorized:
                        status = LoginStatus.CanNotLoginToAccount;
                        break;
                    case HttpStatusCode.NotFound:
                        status = LoginStatus.UserNotFound;
                        break;
                    default:
                        status = LoginStatus.UnexpectedError;
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
                        status = RegistrationStatus.AccountCreated;
                        break;
                    case HttpStatusCode.Unauthorized:
                        status = RegistrationStatus.AccountCreatedTryLoginAgain;
                        break;
                    case HttpStatusCode.Forbidden:
                        status = RegistrationStatus.CanNotCreateAccount;
                        break;
                    default:
                        status = RegistrationStatus.UnexpectedError;
                        break;
                }             
            }
            return status;
        }
    }
}

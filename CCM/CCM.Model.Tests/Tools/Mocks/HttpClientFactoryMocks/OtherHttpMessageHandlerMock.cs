using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class OtherHttpMessageHandlerMock : HttpMessageHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NetworkAuthenticationRequired);
            HttpResponseMessage messageResponse = await Task.FromResult(message);
            return messageResponse;
        }
    }
}

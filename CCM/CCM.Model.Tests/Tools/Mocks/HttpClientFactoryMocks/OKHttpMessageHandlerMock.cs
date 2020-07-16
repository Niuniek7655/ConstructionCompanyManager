using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class OKHttpMessageHandlerMock : HttpMessageHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            HttpResponseMessage messageResponse = await Task.FromResult(message);
            return messageResponse;
        }
    }
}

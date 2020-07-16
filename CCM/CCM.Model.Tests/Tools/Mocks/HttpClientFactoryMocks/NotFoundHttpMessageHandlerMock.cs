using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class NotFoundHttpMessageHandlerMock : HttpMessageHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
            HttpResponseMessage messageResponse = await Task.FromResult(message);
            return messageResponse;
        }
    }
}

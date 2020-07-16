using System.Net.Http;
using System;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class UnauthorizedHttpClientFactoryMock : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            UnauthorizedHttpMessageHandlerMock handlerMock = new UnauthorizedHttpMessageHandlerMock();
            HttpClient client = new HttpClient(handlerMock);
            client.BaseAddress = new Uri("https://www.google.com/");
            return client;
        }
    }
}

using System.Net.Http;
using System;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class ForbiddenHttpClientFactoryMock : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            ForbiddenHttpMessageHandlerMock handlerMock = new ForbiddenHttpMessageHandlerMock();
            HttpClient client = new HttpClient(handlerMock);
            client.BaseAddress = new Uri("https://www.google.com/");
            return client;
        }
    }
}

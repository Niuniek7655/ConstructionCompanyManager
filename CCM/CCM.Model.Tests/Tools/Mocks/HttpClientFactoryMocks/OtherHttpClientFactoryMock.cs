using System.Net.Http;
using System;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class OtherHttpClientFactoryMock : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            OtherHttpMessageHandlerMock handlerMock = new OtherHttpMessageHandlerMock();
            HttpClient client = new HttpClient(handlerMock);
            client.BaseAddress = new Uri("https://www.google.com/");
            return client;
        }
    }
}

using System.Net.Http;
using System;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class OKHttpClientFactoryMock : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            OKHttpMessageHandlerMock handlerMock = new OKHttpMessageHandlerMock();
            HttpClient client = new HttpClient(handlerMock);
            client.BaseAddress = new Uri("https://www.google.com/");
            return client;
        }
    }
}

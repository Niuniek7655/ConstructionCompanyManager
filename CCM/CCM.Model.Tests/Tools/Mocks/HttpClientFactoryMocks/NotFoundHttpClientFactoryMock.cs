using System.Net.Http;
using System;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class NotFoundHttpClientFactoryMock : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            NotFoundHttpMessageHandlerMock handlerMock = new NotFoundHttpMessageHandlerMock();
            HttpClient client = new HttpClient(handlerMock);
            client.BaseAddress = new Uri("https://www.google.com/");
            return client;
        }
    }
}

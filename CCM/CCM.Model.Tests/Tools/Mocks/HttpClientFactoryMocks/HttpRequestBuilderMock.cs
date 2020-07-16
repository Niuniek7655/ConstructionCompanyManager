using CCM.Domain.Tools;
using System.Net.Http;

namespace CCM.Model.Tests.Tools.Mocks.HttpClientFactoryMocks
{
    public class HttpRequestBuilderMock : IHttpRequestBuilder
    {
        public IHttpRequestBuilder AddContent(object contentObject)
        {
            return this;
        }

        public IHttpRequestBuilder AddHeader(IRequestHeader header)
        {
            return this;
        }

        public IHttpRequestBuilder AddHttpMethod(HttpMethod method)
        {
            return this;
        }

        public IHttpRequestBuilder AddUri(string uri)
        {
            return this;
        }

        public T Build<T>() where T : class
        {
            HttpRequestMessage message = new HttpRequestMessage();
            return message as T;
        }
    }
}

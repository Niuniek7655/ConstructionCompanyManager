using System.Net.Http;

namespace CCM.Domain.Tools
{
    public interface IHttpRequestBuilder : IBuilder
    {
        IHttpRequestBuilder AddHttpMethod(HttpMethod method);
        IHttpRequestBuilder AddUri(string uri);
        IHttpRequestBuilder AddHeader(IRequestHeader header);
        IHttpRequestBuilder AddContent(object contentObject);
    }
}

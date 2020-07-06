using CCM.Domain.Tools;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;
using CCM.Constants;
using Microsoft.Extensions.Options;
using CCM.Model.Exceptions;

namespace CCM.Model.Tools
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private HttpRequestMessage _requestMessage;
        private readonly ILogger<HttpRequestBuilder> _logger;
        private readonly HttpRequestBuilderData _data;
        public HttpRequestBuilder(ILogger<HttpRequestBuilder> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            _data = settings.Value.HttpRequestBuilderData;
        }

        public IHttpRequestBuilder AddContent(object contentObject)
        {
            if(_requestMessage == null)
            {
                _logger.LogError(_data.FirstBuildError);
                throw new HttpRequestBuilderException(_data.FirstBuildError);
            }
            if(_requestMessage.Content != null)
            {
                _logger.LogError(_data.FifthBuildError);
                throw new HttpRequestBuilderException(_data.FifthBuildError);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(contentObject, settings);
            StringContent content = new StringContent(json, Encoding.UTF8, _data.MediaType);
            _requestMessage.Content = content;
            return this;
        }

        public IHttpRequestBuilder AddHeader(IRequestHeader header)
        {
            if (_requestMessage == null)
            {
                _logger.LogError(_data.FirstBuildError);
                throw new HttpRequestBuilderException(_data.FirstBuildError);
            }
            _requestMessage.Headers.Add(header.HeaderName, header.HeaderValue);
            return this;
        }

        private HttpMethod httpMethod = null;
        public IHttpRequestBuilder AddHttpMethod(HttpMethod method)
        {
            if(uriString != null)
            {
                _requestMessage = new HttpRequestMessage(method, uriString);
                httpMethod = null;
                uriString = null;
            }
            else
            {
                if(httpMethod != null)
                {
                    _logger.LogError(_data.ThirdBuildError);
                    throw new HttpRequestBuilderException(_data.ThirdBuildError);
                }
                httpMethod = method;
            }
            return this;
        }

        private string uriString = null;
        public IHttpRequestBuilder AddUri(string uri)
        {
            if(httpMethod != null)
            {
                _requestMessage = new HttpRequestMessage(httpMethod, uri);
                httpMethod = null;
                uriString = null;
            }
            else
            {
                if(uriString != null)
                {
                    _logger.LogError(_data.FourthBuildError);
                    throw new HttpRequestBuilderException(_data.FourthBuildError);
                }
                uriString = uri;
            }
            return this;
        }

        public T Build<T>() where T : class
        {
            try
            {
                if (_requestMessage is T)
                {
                    return _requestMessage as T;
                }
                else
                {
                    _logger.LogError(_data.SecondBuildError);
                    throw new HttpRequestBuilderException(_data.SecondBuildError);
                }
            }
            finally
            {
                _requestMessage = null;
            }
        }
    }
}

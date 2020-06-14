using CCM.Domain.Tools;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;

namespace CCM.Model.Tools
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private HttpRequestMessage _requestMessage;

        public HttpRequestBuilder()
        {
            
        }

        private const string firstBuildError = "Add Uri and HTTP method before you add another elements";
        private readonly string mediaType = "application/json";
        public IHttpRequestBuilder AddContent(object contentObject)
        {
            if(_requestMessage == null)
            {
                throw new Exception(firstBuildError);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(contentObject, settings);
            StringContent content = new StringContent(json, Encoding.UTF8, mediaType);
            _requestMessage.Content = content;
            return this;
        }

        public IHttpRequestBuilder AddHeader(IRequestHeader header)
        {
            if (_requestMessage == null)
            {
                throw new Exception(firstBuildError);
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
                uriString = uri;
            }
            return this;
        }

        private const string secondBuildError = "Incorrect type to build object";
        public T Build<T>() where T : class
        {
            if(_requestMessage is T)
            {
                return _requestMessage as T;
            }
            else
            {
                throw new Exception(secondBuildError);
            }
        }
    }
}

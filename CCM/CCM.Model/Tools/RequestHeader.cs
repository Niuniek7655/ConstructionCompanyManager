using CCM.Domain.Tools;

namespace CCM.Model.Tools
{
    public class RequestHeader : IRequestHeader
    {
        public RequestHeader(string headerName, string headerValue)
        {
            HeaderName = headerName;
            HeaderValue = headerValue;
        }

        public string HeaderName { get; private set; }
        public string HeaderValue { get; private set; }
    }
}

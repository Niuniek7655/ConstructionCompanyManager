using System;

namespace CCM.Model.Exceptions
{
    public class HttpRequestBuilderException : ApplicationException
    {
        public HttpRequestBuilderException(string text) : base(text)
        {
        }
    }
}

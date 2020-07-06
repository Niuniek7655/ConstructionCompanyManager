using System;

namespace CCP.Application.Exceptions
{
    public class AcessAPILoggerException : ApplicationException
    {
        public AcessAPILoggerException(string text) : base(text)
        {
        }
    }
}

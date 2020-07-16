using CCM.Model.Tools;
using Microsoft.Extensions.Logging;
using System;

namespace CCM.Model.Tests.Tools.Mocks
{
    public class HttpRequestBuilderLoggerMock : ILogger<HttpRequestBuilder>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            
        }
    }
}

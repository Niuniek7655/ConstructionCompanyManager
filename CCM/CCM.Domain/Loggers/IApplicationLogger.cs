using CCM.Constants;

namespace CCM.Domain.Loggers
{
    public interface IApplicationLogger<T> where T : class
    {
        BasicAccessMessage BasicAccessMessage { get; }
        BasicAccessSenderData BasicAccessSenderData { get; }
        HttpRequestBuilderData HttpRequestBuilderData { get; }
        string IncorrectEnumType { get; }
    }
}

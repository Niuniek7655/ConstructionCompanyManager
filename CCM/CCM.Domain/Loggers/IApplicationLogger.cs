using CCM.Constants;

namespace CCM.Domain.Loggers
{
    public interface IApplicationLogger<T> where T : class
    {
        AccessMessage AccessMessage { get; }
        BasicAccessSenderData BasicAccessSenderData { get; }
        HttpRequestBuilderData HttpRequestBuilderData { get; }
        string IncorrectEnumType { get; }
    }
}

using CCM.Domain.Enums;
namespace CCM.Domain.Loggers
{
    public interface IAcessAPILogger<T> : IApplicationLogger<T> where T : class
    {
        void LogLoginState(LoginStatus status, string login);
        void LogRegistrationState(RegistrationStatus status, string login);
    }
}

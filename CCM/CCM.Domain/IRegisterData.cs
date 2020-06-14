namespace CCM.Domain
{
    public interface IRegisterData
    {
        string Login { get; }
        string Email { get; }
        string Password { get; }
        string ConfimPassword { get; }
    }
}

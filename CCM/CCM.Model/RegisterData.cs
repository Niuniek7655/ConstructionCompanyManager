using CCM.Domain;

namespace CCM.Model
{
    public class RegisterData : IRegisterData
    {
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfimPassword { get; private set; }
    }
}

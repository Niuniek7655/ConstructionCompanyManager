using CCM.Domain;

namespace CCM.Model
{
    public class LoginData : ILoginData
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
    }
}

using CCM.Domain;

namespace CCM.Model
{
    public class LoginData : ILoginData
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

using CCM.Domain;

namespace CCM.Model
{
    public class RegisterData : IRegisterData
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfimPassword { get; set; }
    }
}

using CCM.Domain;

namespace CCM.Model.DTO
{
    public class RegisterDataDTO : IRegisterData
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfimPassword { get; set; }
    }
}

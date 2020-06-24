using CCM.Domain;

namespace CCM.Model.DTO
{
    public class LoginDataDTO : ILoginData
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

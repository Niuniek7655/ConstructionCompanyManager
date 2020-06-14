using AutoMapper;
using CCM.Domain;

namespace CCM.Web.Models
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<LoginViewModel, ILoginData>();
            CreateMap<RegisterViewModel, IRegisterData>();
        }
    }
}

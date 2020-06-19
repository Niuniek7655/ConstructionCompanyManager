using AutoMapper;
using CCM.Model;

namespace CCM.Web.Models
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<LoginViewModel, LoginData>();
            CreateMap<RegisterViewModel, RegisterData>();
        }
    }
}

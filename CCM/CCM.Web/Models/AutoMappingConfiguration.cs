using AutoMapper;
using CCM.Model.DTO;

namespace CCM.Web.Models
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<LoginViewModel, LoginDataDTO>();
            CreateMap<RegisterViewModel, RegisterDataDTO>();
        }
    }
}

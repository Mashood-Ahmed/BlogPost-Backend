using Portfolio.API.Models.Domain;
using AutoMapper;
using Portfolio.API.Models.Domain.Auth;
using Portfolio.API.Models.DTO;

namespace Portfolio.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {

            CreateMap<User, AuthenticateResponse>();

            CreateMap<RegisterRequestDto, User>();

            CreateMap<UpdateUserRequestDto, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;

                    }
                 ));
        }  
    }
}

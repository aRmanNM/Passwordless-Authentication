using AutoMapper;
using PasswordlessAuthentication.Dtos;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, IdentityUser>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.DisplayName));
        }
    }
}
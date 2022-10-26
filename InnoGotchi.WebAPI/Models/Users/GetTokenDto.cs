using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Users.Queries.GetIdentity;

namespace InnoGotchi.WebAPI.Models.Users
{
    public class GetTokenDto : IMapWith<GetIdentityQuery>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetTokenDto, GetIdentityQuery>()
            .ForMember(userCommand => userCommand.Email,
            opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.Password,
            opt => opt.MapFrom(userDto => userDto.Password));
        }
    }
}

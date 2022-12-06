using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Models.Users.Commands.CreateUser;

namespace InnoGotchi.WebAPI.Models.Users
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarBase { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.FirstName,
                opt => opt.MapFrom(userDto => userDto.FirstName))
                .ForMember(userCommand => userCommand.LastName,
                opt => opt.MapFrom(userDto => userDto.LastName))
                .ForMember(userCommand => userCommand.Email,
                opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(userCommand => userCommand.Password,
                opt => opt.MapFrom(userDto => userDto.Password));
        }
    }
}

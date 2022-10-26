using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Users.Commands.ChangePassword;

namespace InnoGotchi.WebAPI.Models.Users
{
    public class ChangePasswordDto : IMapWith<ChangePasswordCommand>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ChangePasswordCommand>()
            .ForMember(userCommand => userCommand.OldPassword,
            opt => opt.MapFrom(userDto => userDto.OldPassword))
            .ForMember(userCommand => userCommand.NewPassword,
            opt => opt.MapFrom(userDto => userDto.NewPassword));
        }
    }
}

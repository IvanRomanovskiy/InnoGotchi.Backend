using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Users.Commands.ChangeAvatar;


namespace InnoGotchi.WebAPI.Models.Users
{
    public class ChangeAvatarDto : IMapWith<ChangeAvatarCommand>
    {
        public byte[] Avatar { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeAvatarDto, ChangeAvatarCommand>()
            .ForMember(userCommand => userCommand.Avatar,
            opt => opt.MapFrom(userDto => userDto.Avatar));
        }
    }
}

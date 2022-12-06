using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Models.Users.Commands.ChangeAvatar;


namespace InnoGotchi.WebAPI.Models.Users
{
    public class ChangeAvatarDto
    {
        public string AvatarBase { get; set; }
    }
}

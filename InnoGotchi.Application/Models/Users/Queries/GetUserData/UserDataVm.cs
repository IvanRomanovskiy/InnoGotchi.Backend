using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Domain;

namespace InnoGotchi.Application.Users.Commands.FindUser
{
    public class UserDataVm : IMapWith<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDataVm>()
                .ForMember(userVm => userVm.FirstName,
                opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userVm => userVm.LastName,
                opt => opt.MapFrom(user => user.LastName))
                .ForMember(userVm => userVm.Email,
                opt => opt.MapFrom(user => user.Email))
                .ForMember(userVm => userVm.Avatar,
                opt => opt.MapFrom(user => user.Avatar));
        }
    }
}
using AutoMapper;
using InnoGotchi.Application.Common.Extentions;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Domain;

namespace InnoGotchi.Application.Models.Users.Queries.GetUserData
{
    public class UserDataVm : IMapWith<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarBase { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDataVm>()
                .ForMember(userVm => userVm.FirstName,
                opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userVm => userVm.LastName,
                opt => opt.MapFrom(user => user.LastName))
                .ForMember(userVm => userVm.Email,
                opt => opt.MapFrom(user => user.Email))
                .ForMember(userVm => userVm.AvatarBase,
                opt => opt.MapFrom(user => user.Avatar.ToBase64String()));
        }
    }
}
using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Models.Users.Commands.ChangeName;

namespace InnoGotchi.WebAPI.Models.Users
{
    public class ChangeNameDto : IMapWith<ChangeNameCommand>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeNameDto, ChangeNameCommand>()
            .ForMember(userCommand => userCommand.FirstName,
            opt => opt.MapFrom(userDto => userDto.FirstName))
            .ForMember(userCommand => userCommand.LastName,
            opt => opt.MapFrom(userDto => userDto.LastName));
        }
    }
}

using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Farms.Commands.CreateFarm;

namespace InnoGotchi.WebAPI.Models.Farm
{
    public class CreateFarmDto : IMapWith<CreateFarmCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFarmDto, CreateFarmCommand>()
                .ForMember(userCommand => userCommand.Name,
                opt => opt.MapFrom(userDto => userDto.Name));
        }

    }
}

using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Models.Pets.Commands.CreatePet;

namespace InnoGotchi.WebAPI.Models.Pets
{
    public class CreatePetDto : IMapWith<CreatePetCommand>
    {
        public string PetName { get; set; }
        public string BodyPath { get; set; }
        public string EyePath { get; set; }
        public string MouthPath { get; set; }
        public string NosePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePetDto, CreatePetCommand>()
                .ForMember(userCommand => userCommand.PetName,
                opt => opt.MapFrom(userDto => userDto.PetName))
                .ForMember(userCommand => userCommand.BodyPath,
                opt => opt.MapFrom(userDto => userDto.BodyPath))
                .ForMember(userCommand => userCommand.EyePath,
                opt => opt.MapFrom(userDto => userDto.EyePath))
                .ForMember(userCommand => userCommand.MouthPath,
                opt => opt.MapFrom(userDto => userDto.MouthPath))
                .ForMember(userCommand => userCommand.NosePath,
                opt => opt.MapFrom(userDto => userDto.NosePath));
        }
    }
}

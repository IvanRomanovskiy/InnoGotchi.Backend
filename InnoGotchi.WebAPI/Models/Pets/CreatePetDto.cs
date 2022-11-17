using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Models.Pets.Commands.CreatePet;

namespace InnoGotchi.WebAPI.Models.Pets
{
    public class CreatePetDto : IMapWith<CreatePetCommand>
    {
        public string PetName { get; set; }
        public Guid BodyId { get; set; }
        public Guid EyeId { get; set; }
        public Guid MouthId { get; set; }
        public Guid NoseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePetDto, CreatePetCommand>()
                .ForMember(userCommand => userCommand.PetName,
                opt => opt.MapFrom(userDto => userDto.PetName))
                .ForMember(userCommand => userCommand.BodyId,
                opt => opt.MapFrom(userDto => userDto.BodyId))
                .ForMember(userCommand => userCommand.EyeId,
                opt => opt.MapFrom(userDto => userDto.EyeId))
                .ForMember(userCommand => userCommand.MouthId,
                opt => opt.MapFrom(userDto => userDto.MouthId))
                .ForMember(userCommand => userCommand.NoseId,
                opt => opt.MapFrom(userDto => userDto.NoseId));
        }
    }
}

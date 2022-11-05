using AutoMapper;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Application.Farms.Commands.AddCollaborator;

namespace InnoGotchi.WebAPI.Models.Farm
{
    public class AddCollaboratorDto : IMapWith<AddCollaboratorCommand>
    {
        public string CollaboratorEmail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddCollaboratorDto, AddCollaboratorCommand>()
                .ForMember(addCollaborator => addCollaborator.CollaboratorEmail,
                opt => opt.MapFrom(collaboratorDto => collaboratorDto.CollaboratorEmail));
        }
    }
}

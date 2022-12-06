using AutoMapper;
using InnoGotchi.Application.Common.Extentions;
using InnoGotchi.Application.Common.Mappings;
using InnoGotchi.Domain;

namespace InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms
{
    public class CollaboratorFarmVm : IMapWith<Farm>
    {
        public string FarmName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerAvatarBase { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Farm, CollaboratorFarmVm>()
                .ForMember(collabVm => collabVm.FarmName,
                opt => opt.MapFrom(farm => farm.Name))
                .ForMember(collabVm => collabVm.OwnerFirstName,
                opt => opt.MapFrom(farm => farm.Owner.FirstName))
                .ForMember(collabVm => collabVm.OwnerLastName,
                opt => opt.MapFrom(farm => farm.Owner.LastName))
                .ForMember(collabVm => collabVm.OwnerAvatarBase,
                opt => opt.MapFrom(farm => farm.Owner.Avatar.ToBase64String()))
                .ForMember(collabVm => collabVm.Pets,
                opt => opt.MapFrom(farm => farm.Pets));
        }

    }
}
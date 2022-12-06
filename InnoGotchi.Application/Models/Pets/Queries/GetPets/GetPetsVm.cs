using InnoGotchi.Domain;

namespace InnoGotchi.Application.Models.Pets.Queries.GetPets
{
    public class GetPetsVm
    {
        public ICollection<Pet> Pets { get; set; }
    }
}
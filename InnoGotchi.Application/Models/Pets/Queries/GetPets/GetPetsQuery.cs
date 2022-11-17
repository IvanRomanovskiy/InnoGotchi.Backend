using InnoGotchi.Domain;
using MediatR;

namespace InnoGotchi.Application.Models.Pets.Queries.GetPets
{
    public class GetPetsQuery : IRequest<GetPetsVm>
    {
        public Guid UserId { get; set; }
    }
}

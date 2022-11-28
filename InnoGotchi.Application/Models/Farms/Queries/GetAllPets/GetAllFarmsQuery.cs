using InnoGotchi.Application.Models.Pets.Queries.GetPets;
using MediatR;

namespace InnoGotchi.Application.Models.Farms.Queries.GetAllPets
{
    public class GetAllFarmsQuery : IRequest<AllFarmsVms>
    {
        public Guid UserId { get; set; }
    }
}

using InnoGotchi.Domain;
using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.FeedPet
{
    public class FeedPetCommand : IRequest<PetStatus>
    {
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
    }
}

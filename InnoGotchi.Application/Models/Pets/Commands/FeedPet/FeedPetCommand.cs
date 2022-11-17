using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.FeedPet
{
    public class FeedPetCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
    }
}

using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.ThirstQuenchingPet
{
    public class ThirstQuenchingPetCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
    }
}

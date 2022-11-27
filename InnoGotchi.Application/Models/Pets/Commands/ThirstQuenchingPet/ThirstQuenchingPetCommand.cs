using InnoGotchi.Domain;
using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.ThirstQuenchingPet
{
    public class ThirstQuenchingPetCommand : IRequest<PetStatus>
    {
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
    }
}

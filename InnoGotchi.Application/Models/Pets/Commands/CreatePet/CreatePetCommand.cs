using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.CreatePet
{
    public class CreatePetCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string PetName { get; set; }
        public Guid BodyId { get; set; }
        public Guid EyeId { get; set; }
        public Guid MouthId { get; set; }
        public Guid NoseId { get; set; }
    }
}

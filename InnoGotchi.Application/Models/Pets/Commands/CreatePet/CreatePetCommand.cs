using MediatR;

namespace InnoGotchi.Application.Models.Pets.Commands.CreatePet
{
    public class CreatePetCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string PetName { get; set; }
        public string BodyPath { get; set; }
        public string EyePath { get; set; }
        public string MouthPath { get; set; }
        public string NosePath { get; set; }
    }
}

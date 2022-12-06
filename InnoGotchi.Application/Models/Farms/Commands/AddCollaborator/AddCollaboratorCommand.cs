
using MediatR;

namespace InnoGotchi.Application.Farms.Commands.AddCollaborator
{
    public class AddCollaboratorCommand : IRequest<Guid>
    {
        public Guid OwnerId { get; set; }
        public string CollaboratorEmail { get; set; }
    }
}

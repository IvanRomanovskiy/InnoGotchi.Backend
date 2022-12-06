using MediatR;

namespace InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms
{
    public class GetCollaboratorFarmsQuery : IRequest<CollaboratorFarmsVm>
    {
        public Guid UserId;
    }
}

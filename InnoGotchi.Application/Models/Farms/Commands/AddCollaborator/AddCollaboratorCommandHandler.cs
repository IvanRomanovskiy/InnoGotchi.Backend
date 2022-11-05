using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Farms.Commands.AddCollaborator
{
    public class AddCollaboratorCommandHandler : IRequestHandler<AddCollaboratorCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;
        private readonly IFarmsDbContext farmsDbContext;
        private readonly ICollaborationDbContext collaborationDbContext;

        public AddCollaboratorCommandHandler(IUsersDbContext usersDbContext, IFarmsDbContext farmsDbContext, ICollaborationDbContext collaborationDbContext)
        {
            this.usersDbContext = usersDbContext;
            this.farmsDbContext = farmsDbContext;
            this.collaborationDbContext = collaborationDbContext;
        }

        public async Task<Guid> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var collaborator = await usersDbContext.Users.FirstOrDefaultAsync(user => user.Email == request.CollaboratorEmail, cancellationToken);
            if (collaborator == null) throw new NotFoundException(nameof(User), request.CollaboratorEmail);
            if (collaborator.Id == request.OwnerId) throw new Exception("You wrote own email");

            var ownFarm = await farmsDbContext.Farms.FirstOrDefaultAsync(farm => farm.Owner.Id == request.OwnerId, cancellationToken);
            if(ownFarm == null) throw new NotFoundException(nameof(Farm), request.OwnerId.ToString());

            var newCollaborator = new Collaboration
            {
                IdOwner = request.OwnerId,
                IdCollaborator = collaborator.Id
            };

            var exist = collaborationDbContext.Collaborations
                .FirstOrDefault(collab => collab.IdCollaborator == collaborator.Id && collab.IdOwner == request.OwnerId);

            if (exist == null)
            {
                collaborationDbContext.Collaborations.Add(newCollaborator);
            }
            else throw new AlreadyExistException(nameof(Collaboration), request.CollaboratorEmail);
            
            await collaborationDbContext.SaveChangesAsync(cancellationToken);
            return newCollaborator.IdOwner;
        }
    }
}

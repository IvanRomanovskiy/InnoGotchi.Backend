using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Farms.Commands.CreateFarm
{
    public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;
        private readonly IFarmsDbContext farmsDbContext;
        public CreateFarmCommandHandler(IUsersDbContext usersDbContext, IFarmsDbContext farmsDbContext)
        {
            this.usersDbContext = usersDbContext;
            this.farmsDbContext = farmsDbContext;
        }

        public async Task<Guid> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            var user = await usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null) throw new NotFoundException(nameof(User), request.UserId.ToString());

            var ownFarm = await farmsDbContext.Farms.FirstOrDefaultAsync(x => x.Owner.Id == request.UserId, cancellationToken);
            var sameNamefarm = await farmsDbContext.Farms.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (ownFarm == null && sameNamefarm == null)
            {
                Farm newFarm = new Farm
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Owner = user,
                    Collaborations = new List<Collaboration>(),
                    Pets = new List<Pet>()
                };
                farmsDbContext.Farms.Add(newFarm);
                await farmsDbContext.SaveChangesAsync(cancellationToken);
                return newFarm.Id;
            }
            throw new AlreadyExistException(nameof(Farm), request.Name.ToString());
        }
    }
}

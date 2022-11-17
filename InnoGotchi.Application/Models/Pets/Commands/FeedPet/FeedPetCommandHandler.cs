using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Pets.Commands.FeedPet
{
    public class FeedPetCommandHandler : IRequestHandler<FeedPetCommand, Guid>
    {
        public IFarmsDbContext farmsDbContext;
        public IPetsDbContext petsDbContext;

        public FeedPetCommandHandler(IFarmsDbContext farmsDbContext, IPetsDbContext petsDbContext)
        {
            this.farmsDbContext = farmsDbContext;
            this.petsDbContext = petsDbContext;
        }

        public async Task<Guid> Handle(FeedPetCommand request, CancellationToken cancellationToken)
        {
            var pet = await petsDbContext.Pets
                .FirstOrDefaultAsync(pet => pet.Id == request.PetId, cancellationToken);
            if (pet == null) throw new NotFoundException(nameof(pet), request.PetId.ToString());

            var farmPet = await farmsDbContext.Farms
                .FirstOrDefaultAsync(farm => farm.Pets.Contains(pet));
            if (farmPet == null) throw new Exception("Pet exists, but farm not found");

            var collab = farmPet.Collaborations
                .FirstOrDefault(collab => collab.IdCollaborator == request.UserId);

            if (farmPet.Owner.Id == request.UserId || collab != null)
                pet.Feed();

            await petsDbContext.SaveChangesAsync(cancellationToken);
            await farmsDbContext.SaveChangesAsync(cancellationToken);
            return request.PetId;
        }
    }
}

using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Pets.Commands.ThirstQuenchingPet
{
    public class ThirstQuenchingPetCommandHandler : IRequestHandler<ThirstQuenchingPetCommand, PetStatus>
    {
        private readonly IFarmsDbContext farmDbContext;
        private readonly IPetsDbContext petsDbContext;
        private readonly ICollaborationDbContext collaborationDbContext;
        private readonly IPetsStatusesDbContext petsStatusesDbContext;

        public ThirstQuenchingPetCommandHandler(IFarmsDbContext farmDbContext,
            IPetsDbContext petsDbContext, ICollaborationDbContext collaborationDbContext, IPetsStatusesDbContext petsStatusesDbContext)
        {
            this.farmDbContext = farmDbContext;
            this.petsDbContext = petsDbContext;
            this.collaborationDbContext = collaborationDbContext;
            this.petsStatusesDbContext = petsStatusesDbContext;
        }

        public async Task<PetStatus> Handle(ThirstQuenchingPetCommand request, CancellationToken cancellationToken)
        {
            var selectedPet = await petsDbContext.Pets
            .FirstOrDefaultAsync(pet => pet.Id == request.PetId, cancellationToken);

            if (selectedPet == null) throw new NotFoundException(nameof(selectedPet), request.PetId.ToString());

            var collabs = collaborationDbContext.Collaborations
            .Where(c => c.IdCollaborator == request.UserId);



            var farms = farmDbContext.Farms.Include(farm => farm.Owner)
            .Where(farm => (farm.Owner.Id == request.UserId) ||
            collabs.FirstOrDefault(c => c.IdOwner == farm.Owner.Id) != null);


            var canDrink = await farms.FirstOrDefaultAsync(farm => farm.Pets.FirstOrDefault(pet => pet.Id == selectedPet.Id) != null);

            if (canDrink == null) throw new Exception("Pet exists, but user don't have permission to them");

            var status = await petsStatusesDbContext.PetsStatuses
            .FirstOrDefaultAsync(stat => stat.Id == selectedPet.Id);

            selectedPet.Status = status;
            selectedPet.Update();
            selectedPet.Drink();
            petsDbContext.Pets.Update(selectedPet);
            await petsDbContext.SaveChangesAsync(cancellationToken);


            return selectedPet.Status;
        }
    }
}

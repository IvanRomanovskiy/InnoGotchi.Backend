using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Pets.Queries.GetPets
{
    public class GetPetsQueryHandler : IRequestHandler<GetPetsQuery, GetPetsVm>
    {
        private readonly IFarmsDbContext farmDbContext;
        private readonly IPetsDbContext petsDbContext;
        private readonly IPetAppearanceDbContext petAppearanceDbContext;
        private readonly IPetsStatusesDbContext statusesDbContext;

        public GetPetsQueryHandler(IFarmsDbContext farmDbContext, 
            IPetsDbContext petsDbContext, IPetAppearanceDbContext petAppearanceDbContext,IPetsStatusesDbContext statusesDbContext)
        {
            this.farmDbContext = farmDbContext;
            this.petsDbContext = petsDbContext;
            this.petAppearanceDbContext = petAppearanceDbContext;
            this.statusesDbContext = statusesDbContext;
        }

        public async Task<GetPetsVm> Handle(GetPetsQuery request, CancellationToken cancellationToken)
        {
            var farm = await farmDbContext.Farms.Include(p => p.Pets)
            .FirstOrDefaultAsync(f => f.Owner.Id == request.UserId, cancellationToken);
            if (farm == null) throw new NotFoundException(nameof(farm), request.UserId.ToString());


            GetPetsVm vm = new GetPetsVm()
            {
                Pets = new List<Pet>()
            };

            if (farm.Pets == null) return vm;

            foreach (var pet in farm.Pets)
            {
                pet.Appearance = petAppearanceDbContext.PetAppearances
                    .Include(p => p.Nose).Include(p => p.Body).Include(p => p.Eye).Include(p => p.Mouth)
                    .FirstOrDefault(appearance => appearance.Id == pet.Id) ?? new PetAppearance();
                pet.Status = statusesDbContext.PetsStatuses
                    .FirstOrDefault(status => status.Id == pet.Id) ?? new PetStatus();

                pet.Update();
                vm.Pets.Add(pet);
                petsDbContext.Pets.Update(pet);
            }
            await petsDbContext.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}

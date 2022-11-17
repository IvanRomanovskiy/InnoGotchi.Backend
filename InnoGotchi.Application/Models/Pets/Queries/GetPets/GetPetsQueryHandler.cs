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

        public GetPetsQueryHandler(IFarmsDbContext farmDbContext)
        {
            this.farmDbContext = farmDbContext;
        }

        public async Task<GetPetsVm> Handle(GetPetsQuery request, CancellationToken cancellationToken)
        {
            var farm = await farmDbContext.Farms
            .FirstOrDefaultAsync(f => f.Owner.Id == request.UserId, cancellationToken);
            if (farm == null) throw new NotFoundException(nameof(farm), request.UserId.ToString());

            GetPetsVm vm = new GetPetsVm()
            {
                Pets = new List<Pet>()
            };

            if (farm.Pets == null) return vm;

            foreach (var pet in farm.Pets)
            {
                pet.Update();
                vm.Pets.Add(pet);
            }
            await farmDbContext.SaveChangesAsync(cancellationToken);

            return vm;
        }
    }
}

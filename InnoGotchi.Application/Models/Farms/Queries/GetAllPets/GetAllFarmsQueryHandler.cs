using AutoMapper;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Farms.Queries.GetAllPets
{
    public class GetAllFarmsQueryHandler : IRequestHandler<GetAllFarmsQuery, AllFarmsVms>
    {
        private readonly IFarmsDbContext farmDbContext;
        private readonly IPetsDbContext petsDbContext;
        private readonly IPetAppearanceDbContext petAppearanceDbContext;
        private readonly IPetsStatusesDbContext statusesDbContext;
        private readonly IMapper mapper;
        public GetAllFarmsQueryHandler(IFarmsDbContext farmDbContext, IPetsDbContext petsDbContext, 
            IPetAppearanceDbContext petAppearanceDbContext, IPetsStatusesDbContext statusesDbContext, IMapper mapper)
        {
            this.farmDbContext = farmDbContext;
            this.petsDbContext = petsDbContext;
            this.petAppearanceDbContext = petAppearanceDbContext;
            this.statusesDbContext = statusesDbContext;
            this.mapper = mapper;
        }

        public async Task<AllFarmsVms> Handle(GetAllFarmsQuery request, CancellationToken cancellationToken)
        {
            AllFarmsVms result = new AllFarmsVms();
            result.UserFarmsVm = new List<AllFarmsVm>();


            var farms = farmDbContext.Farms.Include(o => o.Owner).Include(p => p.Pets)
                .Where(farm => farm.Owner.Id != request.UserId ).ToList();

            foreach (var farm in farms)
            {
                var vm = mapper.Map<AllFarmsVm>(farm);

                foreach (var pet in vm.Pets)
                {
                    pet.Appearance = petAppearanceDbContext.PetAppearances
                    .Include(p => p.Nose).Include(p => p.Body).Include(p => p.Eye).Include(p => p.Mouth)
                    .FirstOrDefault(appearance => appearance.Id == pet.Id) ?? new PetAppearance();
                    pet.Status = statusesDbContext.PetsStatuses
                    .FirstOrDefault(status => status.Id == pet.Id) ?? new PetStatus();

                    pet.Update();
                    petsDbContext.Pets.Update(pet);
                }

                result.UserFarmsVm.Add(vm);
            }
            return result;
        }
    }
}

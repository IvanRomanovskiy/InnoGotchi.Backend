using AutoMapper;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms;
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
                .Where(farm => farm.Owner.Id != request.UserId );

            foreach (var farm in farms)
            {
                var vm = mapper.Map<AllFarmsVm>(farm);
                vm.Pets = (from p in vm.Pets
                           join s in statusesDbContext.PetsStatuses on p.Id equals s.Id
                           join a in petAppearanceDbContext.PetAppearances
                           .Include(p => p.Nose).Include(p => p.Body).Include(p => p.Eye).Include(p => p.Mouth)
                           on p.Id equals a.Id
                select p).ToList();

                foreach (var pet in vm.Pets)
                {
                    pet.Update();
                    petsDbContext.Pets.Update(pet);
                }

                result.UserFarmsVm.Add(vm);
            }
            return result;
        }
    }
}

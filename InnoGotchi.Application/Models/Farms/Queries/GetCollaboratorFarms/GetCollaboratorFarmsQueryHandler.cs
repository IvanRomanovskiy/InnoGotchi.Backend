using AutoMapper;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms
{
    public class GetCollaboratorFarmsQueryHandler : IRequestHandler<GetCollaboratorFarmsQuery,CollaboratorFarmsVm>
    {
        private readonly IFarmsDbContext farmsDbContext;
        private readonly IMapper mapper;
        private readonly ICollaborationDbContext collaborationDbContext;
        private readonly IPetAppearanceDbContext petAppearanceDbContext;
        private readonly IPetsStatusesDbContext petsStatusesDbContext;
        private readonly IPetsDbContext petsDbContext;

        public GetCollaboratorFarmsQueryHandler(IFarmsDbContext farmsDbContext, IMapper mapper,
            ICollaborationDbContext collaborationDbContext,IPetAppearanceDbContext petAppearanceDbContext,
            IPetsStatusesDbContext petsStatusesDbContext,IPetsDbContext petsDbContext)
        {
            this.farmsDbContext = farmsDbContext;
            this.mapper = mapper;
            this.collaborationDbContext = collaborationDbContext;
            this.petAppearanceDbContext = petAppearanceDbContext;
            this.petsStatusesDbContext = petsStatusesDbContext;
            this.petsDbContext = petsDbContext;
        }

        public async Task<CollaboratorFarmsVm> Handle(GetCollaboratorFarmsQuery request, CancellationToken cancellationToken)
        {
            CollaboratorFarmsVm result = new CollaboratorFarmsVm();
            result.collaboratorFarmsVm = new List<CollaboratorFarmVm>();
 
                var CollaboratorIds = collaborationDbContext.Collaborations.Where(collab => collab.IdCollaborator == request.UserId);

                var farms = farmsDbContext.Farms
                    .Where(farm => CollaboratorIds.Select(collab => collab.IdOwner).Contains(farm.Owner.Id))
                    .Include(o => o.Owner).Include(p => p.Pets);

                foreach (var farm in farms)
                {
                    var vm = mapper.Map<CollaboratorFarmVm>(farm);

                vm.Pets = (from p in vm.Pets
                           join s in petsStatusesDbContext.PetsStatuses on p.Id equals s.Id
                           join a in petAppearanceDbContext.PetAppearances
                           .Include(p => p.Nose).Include(p => p.Body).Include(p => p.Eye).Include(p => p.Mouth)
                           on p.Id equals a.Id
                           select p).ToList();

                foreach (var pet in vm.Pets)
                {
                    pet.Update();
                    petsDbContext.Pets.Update(pet);
                }

                    result.collaboratorFarmsVm.Add(vm);
                }

  


            return result;


        }
    }
}

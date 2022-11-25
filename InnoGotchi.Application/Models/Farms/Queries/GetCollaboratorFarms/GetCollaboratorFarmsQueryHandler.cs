using AutoMapper;
using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms
{
    public class GetCollaboratorFarmsQueryHandler : IRequestHandler<GetCollaboratorFarmsQuery,CollaboratorFarmsVm>
    {
        private readonly IFarmsDbContext farmsDbContext;
        private readonly IMapper mapper;
        private readonly ICollaborationDbContext collaborationDbContext;
        public GetCollaboratorFarmsQueryHandler(
            IFarmsDbContext farmsDbContext, IMapper mapper, ICollaborationDbContext collaborationDbContext)
        {
            this.farmsDbContext = farmsDbContext;
            this.mapper = mapper;
            this.collaborationDbContext = collaborationDbContext;
        }

        public async Task<CollaboratorFarmsVm> Handle(GetCollaboratorFarmsQuery request, CancellationToken cancellationToken)
        {
            CollaboratorFarmsVm result = new CollaboratorFarmsVm();
            result.collaboratorFarmsVm = new List<CollaboratorFarmVm>();
            try
            {
                var CollaboratorIds = collaborationDbContext.Collaborations.Where(collab => collab.IdCollaborator == request.UserId);

                var farms = farmsDbContext.Farms
                    .Where(farm => CollaboratorIds.Select(collab => collab.IdOwner).Contains(farm.Owner.Id))
                    .Include(o => o.Owner).Include(p => p.Pets);

                foreach (var farm in farms)
                {
                    var vm = mapper.Map<CollaboratorFarmVm>(farm);
                    result.collaboratorFarmsVm.Add(vm);
                }

            }
            catch { }


            return result;


        }
    }
}

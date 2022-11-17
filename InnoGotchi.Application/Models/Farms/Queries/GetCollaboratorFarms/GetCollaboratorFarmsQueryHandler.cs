using AutoMapper;
using InnoGotchi.Application.Interfaces;
using MediatR;

namespace InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms
{
    public class GetCollaboratorFarmsQueryHandler : IRequestHandler<GetCollaboratorFarmsQuery,CollaboratorFarmsVm>
    {
        private IFarmsDbContext farmsDbContext;
        private readonly IMapper mapper;
        public async Task<CollaboratorFarmsVm> Handle(GetCollaboratorFarmsQuery request, CancellationToken cancellationToken)
        {
            var farms = farmsDbContext.Farms
                .Where(farm => farm.Collaborations
                       .FirstOrDefault(collab => collab.IdCollaborator == request.UserId) != null);

            CollaboratorFarmsVm result = new CollaboratorFarmsVm();
            result.collaboratorFarmsVm = new List<CollaboratorFarmVm>();
            foreach (var farm in farms)
                result.collaboratorFarmsVm.Add(mapper.Map<CollaboratorFarmVm>(farm));

            return result;

        }
    }
}

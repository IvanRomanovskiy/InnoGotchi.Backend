using MediatR;

namespace InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo
{
    public class GetFarmInfoQuery : IRequest<FarmInfoVm>
    {
        public Guid UserId { get; set; }
    }
}

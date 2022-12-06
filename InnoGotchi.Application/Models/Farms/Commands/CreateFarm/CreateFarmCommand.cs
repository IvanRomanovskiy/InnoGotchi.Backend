using MediatR;


namespace InnoGotchi.Application.Farms.Commands.CreateFarm
{
    public class CreateFarmCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}

using MediatR;

namespace InnoGotchi.Application.Models.Users.Commands.ChangeName
{
    public class ChangeNameCommand :IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
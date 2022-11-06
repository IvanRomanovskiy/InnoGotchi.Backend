using MediatR;

namespace InnoGotchi.Application.Models.Users.Commands.FindUser
{
    public class GetUserTokenQuery: IRequest<UserDataVm>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}

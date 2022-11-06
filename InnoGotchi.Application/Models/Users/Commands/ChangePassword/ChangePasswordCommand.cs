using MediatR;

namespace InnoGotchi.Application.Models.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

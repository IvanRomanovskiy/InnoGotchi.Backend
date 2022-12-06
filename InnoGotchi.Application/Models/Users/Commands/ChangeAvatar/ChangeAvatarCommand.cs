using MediatR;

namespace InnoGotchi.Application.Models.Users.Commands.ChangeAvatar
{
    public class ChangeAvatarCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public byte[] Avatar { get; set; }
    }
}

using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Users.Commands.ChangeAvatar
{
    public class ChangeAvatarCommandHandler : IRequestHandler<ChangeAvatarCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;

        public ChangeAvatarCommandHandler(IUsersDbContext usersDbContext) => this.usersDbContext = usersDbContext;

        public async Task<Guid> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken).Result;
            if (user == null) throw new Exception("User not found");
            user.Avatar = request.Avatar;
            await usersDbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}

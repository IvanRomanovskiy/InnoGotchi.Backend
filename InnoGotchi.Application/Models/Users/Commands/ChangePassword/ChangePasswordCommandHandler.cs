using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Common.Extentions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;
        public ChangePasswordCommandHandler(IUsersDbContext usersDbContext) => this.usersDbContext = usersDbContext;

        public async Task<Guid> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await usersDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            if(user == null) throw new NotFoundException(nameof(User), request.Id.ToString());
            if (user.Password.Equals(request.OldPassword.ToShaHash()))
            {
                user.Password = request.NewPassword;
                await usersDbContext.SaveChangesAsync(cancellationToken);
            }
            else return Guid.Empty;

            return user.Id;
        }
    }
}

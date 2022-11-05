using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Users.Commands.ChangeName
{
    public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;
        public ChangeNameCommandHandler(IUsersDbContext usersDbContext) => this.usersDbContext = usersDbContext;
        public async Task<Guid> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
        {
            var user = usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken).Result;
            if (user == null) throw new Exception("User not found");
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            await usersDbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}

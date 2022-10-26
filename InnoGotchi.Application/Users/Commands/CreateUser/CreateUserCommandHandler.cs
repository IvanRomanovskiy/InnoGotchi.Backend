using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Common.Extentions;

namespace InnoGotchi.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUsersDbContext usersDbContext;
        public CreateUserCommandHandler(IUsersDbContext usersDbContext) => this.usersDbContext = usersDbContext;
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await usersDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if(user == null)
            {
                User newUser = new User
                {                    
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password.ToShaHash(),
                    Avatar = request.Avatar,
                    Role = "user"
                };
                usersDbContext.Users.Add(newUser);
                await usersDbContext.SaveChangesAsync(cancellationToken);
                return newUser.Id;
            }
            throw new AlreadyExistException(nameof(User),request.Email);
        }
    }
}

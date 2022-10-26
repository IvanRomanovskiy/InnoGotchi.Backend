using InnoGotchi.Application.Common.Extentions;
using InnoGotchi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InnoGotchi.Application.Users.Queries.GetIdentity
{
    public class GetIdentityQueryHandler
        : IRequestHandler<GetIdentityQuery, ClaimsIdentity>
    {
        private readonly IUsersDbContext dbContext;

        public GetIdentityQueryHandler(IUsersDbContext dbContext) => this.dbContext = dbContext;

        public async Task<ClaimsIdentity?> Handle(GetIdentityQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password.ToShaHash());
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}

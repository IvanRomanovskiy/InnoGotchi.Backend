using MediatR;
using System.Security.Claims;


namespace InnoGotchi.Application.Models.Users.Queries.GetIdentity
{
    public class GetIdentityQuery : IRequest<ClaimsIdentity>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

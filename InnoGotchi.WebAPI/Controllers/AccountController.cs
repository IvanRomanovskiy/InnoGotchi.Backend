using InnoGotchi.Domain;
using InnoGotchi.Persistence;
using InnoGotchi.WebAPI.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using InnoGotchi.WebAPI.Properties;
namespace InnoGotchi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private InnoGotchiDbContext context;

        public AccountController(InnoGotchiDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                if (model.Avatar is null)
                {
                    model.Avatar = Resources.DefaultAvatar1;
                }
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    Avatar = model.Avatar,
                    Role = "user"
                });
                await context.SaveChangesAsync();
                return Accepted();
            }
            return BadRequest(new { errorText = "User already exists" });         
        }



        [HttpGet("/token")]
        public async Task<IActionResult> Token(LoginModel model)
        {
            var identity = await GetIdentity(model);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromHours(24)),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt
            };

            return new JsonResult(response);
        }

        private async Task<ClaimsIdentity?> GetIdentity(LoginModel model)
        {

            
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using InnoGotchi.WebAPI.Properties;
using InnoGotchi.Application.Users.Commands.CreateUser;
using AutoMapper;
using InnoGotchi.WebAPI.Common.Extentions;
using InnoGotchi.Application.Users.Queries.GetIdentity;
using InnoGotchi.Application.Users.Commands.ChangeName;
using InnoGotchi.Application.Users.Commands.ChangePassword;
using InnoGotchi.WebAPI.Models.Users;

namespace InnoGotchi.WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMapper mapper;

        public AccountController(IMapper mapper) => this.mapper = mapper;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            createUserDto.Avatar = Extentions.TryScaleImage(createUserDto.Avatar) ?? Resources.DefaultAvatar;

            var command = mapper.Map<CreateUserCommand>(createUserDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeName([FromBody] ChangeNameDto changeNameDto)
        {
            var command = mapper.Map<ChangeNameCommand>(changeNameDto);
            command.Id = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var command = mapper.Map<ChangePasswordCommand>(changePasswordDto);
            command.Id = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar([FromBody] ChangeAvatarDto changeAvatarDto)
        {
            var Avatar = Extentions.TryScaleImage(changeAvatarDto.Avatar);
            if (Avatar == null) return BadRequest();
            changeAvatarDto.Avatar = Avatar;
            var command = mapper.Map<ChangePasswordCommand>(changeAvatarDto);
            command.Id = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpGet]
        public async Task<IActionResult> GetToken([FromBody] GetTokenDto getTokenDto)
        {
            var query = mapper.Map<GetIdentityQuery>(getTokenDto);
            var identity = await Mediator.Send(query);
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
    }
}

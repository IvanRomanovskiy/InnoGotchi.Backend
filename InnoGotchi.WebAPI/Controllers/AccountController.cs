using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using InnoGotchi.WebAPI.Properties;
using InnoGotchi.Application.Models.Users.Commands.CreateUser;
using AutoMapper;
using InnoGotchi.WebAPI.Common.Extentions;
using InnoGotchi.Application.Models.Users.Queries.GetIdentity;
using InnoGotchi.Application.Models.Users.Commands.ChangeName;
using InnoGotchi.Application.Models.Users.Commands.ChangePassword;
using InnoGotchi.WebAPI.Models.Users;
using InnoGotchi.Application.Models.Users.Queries.GetUserData;
using InnoGotchi.Application.Models.Users.Commands.ChangeAvatar;
using System.Text;
using System.Text.Json;

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
            var Avatar = Extentions.TryScaleImage(createUserDto.AvatarBase) ?? Resources.DefaultAvatar;

            var command = mapper.Map<CreateUserCommand>(createUserDto);
            command.Avatar = Avatar;
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
            if(userId == Guid.Empty) return BadRequest(userId);
            else return Ok(userId);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar([FromBody] ChangeAvatarDto changeAvatarDto)
        {
            var Avatar = Extentions.TryScaleImage(changeAvatarDto.AvatarBase);
            if (Avatar == null) return BadRequest();

            var command = new ChangeAvatarCommand
            {
                Id = UserId,
                Avatar = Avatar
            };
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpPost]
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var query = new GetUserDataQuery()
            {
                Id = UserId
            };
            var result = await Mediator.Send(query);
            string stringData = JsonSerializer.Serialize<UserDataVm>(result);
            return Ok(stringData);
        }

    }
}

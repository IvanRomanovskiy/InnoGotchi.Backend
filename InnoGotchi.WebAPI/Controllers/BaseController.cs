using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnoGotchi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator =>
            mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);
              
    }
}

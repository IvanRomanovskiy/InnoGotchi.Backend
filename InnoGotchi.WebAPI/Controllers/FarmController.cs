using AutoMapper;
using InnoGotchi.Application.Farms.Commands.AddCollaborator;
using InnoGotchi.Application.Farms.Commands.CreateFarm;
using InnoGotchi.Application.Models.Farms.Queries.GetAllPets;
using InnoGotchi.Application.Models.Farms.Queries.GetCollaboratorFarms;
using InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo;
using InnoGotchi.WebAPI.Models.Farm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WebAPI.Controllers
{
    public class FarmController : BaseController
    {
        private readonly IMapper mapper;

        public FarmController(IMapper mapper) => this.mapper = mapper;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFarm([FromBody] CreateFarmDto createFarmDto)
        {
            var command = mapper.Map<CreateFarmCommand>(createFarmDto);
            command.UserId = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCollaborator([FromBody] AddCollaboratorDto createFarmDto)
        {
            var command = mapper.Map<AddCollaboratorCommand>(createFarmDto);
            command.OwnerId = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFarmInfo()
        {
            var query = new GetFarmInfoQuery
            {
                UserId = this.UserId
            };
            var farmInfoVm = await Mediator.Send(query);
            return new JsonResult(farmInfoVm);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFarms()
        {
            var query = new GetAllFarmsQuery
            {
                UserId = this.UserId
            };
            var farms = await Mediator.Send(query);
            return new JsonResult(farms);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCollaboratiorFarms()
        {
            var query = new GetCollaboratorFarmsQuery
            {
                UserId = this.UserId
            };
            var collaboratorFarmVms = await Mediator.Send(query);



            return new JsonResult(collaboratorFarmVms);
        }

    }
}

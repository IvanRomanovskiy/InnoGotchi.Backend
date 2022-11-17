using AutoMapper;
using InnoGotchi.Application.Models.Pets.Commands.CreatePet;
using InnoGotchi.Application.Models.Pets.Commands.FeedPet;
using InnoGotchi.Application.Models.Pets.Commands.ThirstQuenchingPet;
using InnoGotchi.Application.Models.Pets.Queries.GetPets;
using InnoGotchi.WebAPI.Models.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InnoGotchi.WebAPI.Controllers
{
    public class PetController : BaseController
    {
        private readonly IMapper mapper;

        public PetController(IMapper mapper) => this.mapper = mapper;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDto createPetDto)
        {
            var command = mapper.Map<CreatePetCommand>(createPetDto);
            command.UserId = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPets()
        {
            GetPetsQuery query = new GetPetsQuery() { UserId = UserId };
            var result =await Mediator.Send(query);

            return new JsonResult(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> FeedPet(Guid PetId)
        {
            FeedPetCommand command = new FeedPetCommand()
            {
                UserId = UserId,
                PetId = PetId
            };
            var result = Mediator.Send(command);

            return Ok(PetId);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ThirstQuenchingPet(Guid PetId)
        {
            ThirstQuenchingPetCommand command = new ThirstQuenchingPetCommand()
            {
                UserId = UserId,
                PetId = PetId
            };
            var result = Mediator.Send(command);

            return Ok(PetId);
        }
    }
}

﻿using AutoMapper;
using InnoGotchi.Application.Farms.Commands.AddCollaborator;
using InnoGotchi.Application.Farms.Commands.CreateFarm;
using InnoGotchi.Application.Models.Farms.Queries.GetFarmInfo;
using InnoGotchi.Domain;
using InnoGotchi.WebAPI.Models.Farm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    }
}

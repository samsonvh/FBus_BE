﻿using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinationsController : ControllerBase
    {
        private readonly ICoordinationService _coordinationService;

        public CoordinationsController(ICoordinationService coordinationService)
        {
            _coordinationService = coordinationService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCoordinationList([FromQuery] CoordinationPageRequest pageRequest)
        {
            return Ok(await _coordinationService.GetCoordinationList(pageRequest));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCoordinationDetails([FromRoute] int id)
        {
            return Ok(await _coordinationService.GetCoordinationDetails(id));
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CoordinationInputDTO coordinationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _coordinationService.Create(userId, coordinationInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CoordinationInputDTO coordinationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _coordinationService.Update(userId, coordinationInputDTO, id));
        }

        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _coordinationService.ChangeStatus(id, status));
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _coordinationService.Deactivate(id));
        }
    }
}

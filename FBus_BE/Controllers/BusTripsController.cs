using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusTripsController : ControllerBase
    {
        private readonly IBusTripService _busTripService;
        public BusTripsController(IBusTripService busTripService)
        {
            _busTripService = busTripService;
        }

        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusTripDetails([FromRoute] int id)
        {
            return Ok(await _busTripService.GetBusTripDetails(id));
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BusTripInputDTO busTripInputDTO)
        {
            return Ok(await _busTripService.Create(busTripInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] BusTripInputDTO busTripInputDTO)
        {
            return Ok(await _busTripService.Update(id, busTripInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _busTripService.ChangeStatus(id, status));
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactive([FromRoute] int id)
        {
            return Ok(await _busTripService.Deactivate(id));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Services;
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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusTripDTO))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusTripDetails(int id)
        {
            return Ok(await _busTripService.GetBusById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BusTripDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("coordination/{coordinationId:int}")]
        public async Task<IActionResult> GetBusTripsByCoordinationId(int coordinationId)
        {
            var busTrips = await _busTripService.GetBusByCoordinationId(coordinationId);

            if (busTrips != null && busTrips.Count > 0)
            {
                return Ok(busTrips);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BusTripDTO))]
        [HttpPost]
        public async Task<IActionResult> CreateNewBus([FromBody] BusTripInputDTO busTripInputDTO)
        {
            return CreatedAtAction(null, await _busTripService.Create(busTripInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusTripDTO))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BusTripInputDTO busTripInputDTO)
        {
            return Ok(await _busTripService.Update(id, busTripInputDTO));
        }

    }
}
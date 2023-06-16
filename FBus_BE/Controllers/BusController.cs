using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.Dto;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Models;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBusById(int id)
        {
            var bus = await _busService.GetBusById(id);
            if (bus == null)
            {
                return NotFound();
            }
            return Ok(bus);
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BusDTO))]
        [HttpPost]
        public async Task<IActionResult> CreateNewBus([FromBody] BusInputDTO busInputDTO)
        {
            return CreatedAtAction(null, await _busService.Create(busInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BusInputDTO busInputDTO)
        {
            return Ok(await _busService.Update(id, busInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            return Ok(await _busService.Deactivate(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Activate(int id)
        {
            return Ok(await _busService.Activate(id));
        }
    }
}
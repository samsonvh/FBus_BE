using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.Dto;
using FBus_BE.Models;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/buses")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        private readonly IMapper _mapper;

        public BusController(IBusService service, IMapper mapper)
        {
            _busService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBuses()
        {
            var buses = await _busService.GetAllBus();
            if (buses == null)
                return BadRequest();
            return Ok(buses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBusById(short id)
        {
            var buses = await _busService.GetBusById(id);
            if (buses == null)
                return NotFound();
            var busDto = _mapper.Map<BusDto>(buses);
            return Ok(busDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewBus([FromBody] BusDto busDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bus = _mapper.Map<Bus>(busDto);
            await _busService.Create(bus);
            var createdBusDto = _mapper.Map<BusDto>(bus);
            return CreatedAtAction(nameof(GetBusById), new { id = createdBusDto.Id }, createdBusDto);
        }
    }
}
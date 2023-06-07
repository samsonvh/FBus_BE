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
    [Route("api/[controller]")]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;
        public StationController(IStationService stationService, IMapper mapper)
        {
            _stationService = stationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStation()
        {
            var stations = await _stationService.GetAllStation();
            if (stations == null)
            {
                return BadRequest();
            }
            var stationDto = _mapper.Map<IEnumerable<StationDto>>(stations);
            return Ok(stationDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStationById(short id)
        {
            var station = await _stationService.GetStationById(id);
            if (station == null)
            {
                return NotFound();
            }
            var stationDto = _mapper.Map<StationDto>(station);
            return Ok(stationDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewStation([FromBody] StationDto stationDto)
        {
            if (!ModelState.IsValid || stationDto == null)
            {
                return BadRequest("Invalid Data");
            }

            var newStationDto = _mapper.Map<StationDto>(await _stationService.Create(_mapper.Map<Station>(stationDto)));

            return CreatedAtAction(nameof(GetStationById), new { id = newStationDto.Id }, newStationDto);
        }
    }
}
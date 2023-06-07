using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.Dto;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IMapper _mapper;
        public RouteController(IRouteService routeService, IMapper mapper)
        {
            _routeService = routeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var routes = await _routeService.GetRouteList();
            if (routes == null)
            {
                return BadRequest();
            }
            var routeDto = _mapper.Map<IEnumerable<RouteDto>>(routes);
            return Ok(routeDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRouteById(short id)
        {
            var route = await _routeService.GetRouteById(id);
            if (route == null)
            {
                return NotFound();
            }
            var routeDto = _mapper.Map<RouteDto>(route);
            return Ok(routeDto);
        }

        [HttpPost]
        public async Task<IActionResult> createNewRoute([FromBody] RouteDto routeDto)
        {
            if (!ModelState.IsValid || routeDto == null)
            {
                return BadRequest("Invalid Data");
            }

            var newRouteDto = _mapper.Map<RouteDto>(await _routeService.Create(_mapper.Map<Models.Route>(routeDto)));

            return CreatedAtAction(nameof(GetRouteById), new { id = newRouteDto.Id }, newRouteDto);
        }
    }
}
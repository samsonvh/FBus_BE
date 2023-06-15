using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<RouteListingDTO>))]
        [HttpGet]
        public async Task<IActionResult> GetRouteList([FromQuery] RoutePageRequest pageRequest)
        {
            return Ok(await _routeService.GetRoutesWithPaging(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RouteDTO))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRouteDetails([FromRoute] int id)
        {
            return Ok(await _routeService.GetRouteDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RouteDTO))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RouteInputDTO routeInputDTO)
        {
            return CreatedAtAction(null, await _routeService.Create(routeInputDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RouteInputDTO routeInputDTO)
        {
            return Ok(await _routeService.Update(id, routeInputDTO));
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _routeService.ChangeStatus(id, status));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _routeService.Deactivate(id));
        }
    }
}

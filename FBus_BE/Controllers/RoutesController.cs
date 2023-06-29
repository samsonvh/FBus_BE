using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultPageResponse<RouteListingDTO>))]
        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetRouteList([FromQuery] RoutePageRequest pageRequest)
        {
            return Ok(await _routeService.GetRouteList(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RouteDTO))]
        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRouteDetails([FromRoute] int id)
        {
            return Ok(await _routeService.GetRouteDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RouteDTO))]
        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RouteInputDTO routeInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _routeService.Create(userId, routeInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RouteDTO))]
        [Authorize("AdminOnly")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RouteInputDTO routeInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _routeService.Update(userId, routeInputDTO, id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _routeService.ChangeStatus(id, status));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _routeService.Deactivate(id));
        }
    }
}

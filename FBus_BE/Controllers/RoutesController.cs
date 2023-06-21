using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.PageRequests;
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

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetRouteList([FromRoute] RoutePageRequest pageRequest)
        {
            return Ok(await _routeService.GetRouteList(pageRequest));
        }

        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRouteDetails([FromRoute] int id)
        {
            return Ok(await _routeService.GetRouteDetails(id));
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RouteInputDTO routeInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _routeService.Create(userId, routeInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RouteInputDTO routeInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _routeService.Update(userId, routeInputDTO, id));
        }

        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _routeService.ChangeStatus(id, status));
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _routeService.Deactivate(id));
        }
    }
}

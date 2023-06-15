using FBus_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteStationsController : ControllerBase
    {
        private readonly IRouteStationService _routeStationService;

        public RouteStationsController(IRouteStationService routeStationService)
        {
            _routeStationService = routeStationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRouteStationList([FromQuery] int routeId)
        {
            return Ok(await _routeStationService.GetRouteStationList(routeId));
        }
    }
}

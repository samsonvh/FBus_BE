using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/route-station")]
    public class RouteStationController : ControllerBase
    {
        private readonly IRouteStationService _routeStationService;
        public RouteStationController(IRouteStationService routeStationService)
        {
            _routeStationService = routeStationService;
        }

        [HttpGet("{routeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindByRoute(short routeId)
        {
            var routeStation = await _routeStationService.GetByRouteId(routeId);
            return routeStation == null ? NotFound() : Ok(routeStation);
        }

    }
}
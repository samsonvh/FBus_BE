using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinationStatusesController : ControllerBase
    {
        private readonly ICoordinationStatusService _coordinationStatusService;
        public CoordinationStatusesController(ICoordinationStatusService coordinationStatusService)
        {
            _coordinationStatusService = coordinationStatusService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCoordinationStatuesById(int id)
        {
            return Ok(await _coordinationStatusService.GetCoordinationById(id));
        }
    }
}
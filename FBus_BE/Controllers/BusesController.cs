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
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusesController(IBusService busService)
        {
            _busService = busService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultPageResponse<BusListingDTO>))]
        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetBusList([FromQuery] BusPageRequest pageRequest)
        {
            return Ok(await _busService.GetBusList(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusDTO))]
        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusDetails([FromRoute] int id)
        {
            return Ok(await _busService.GetBusDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusDTO))]
        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BusInputDTO busInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _busService.Create(userId, busInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusDTO))]
        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] BusInputDTO busInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _busService.Update(userId, busInputDTO, id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _busService.ChangeStatus(id, status));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _busService.Deactivate(id));
        }
    }
}

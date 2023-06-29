using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DefaultPageResponse<StationListingDTO>))]
        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetStationList([FromQuery] StationPageRequest pageRequest)
        {
            return Ok(await _stationService.GetStationList(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StationDTO))]
        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStationDetails([FromRoute] int id)
        {
            return Ok(await _stationService.GetStationDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StationDTO))]
        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StationInputDTO stationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _stationService.Create(userId, stationInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StationDTO))]
        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] StationInputDTO stationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _stationService.Update(userId, stationInputDTO, id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _stationService.ChangeStatus(id, status));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [Authorize("AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _stationService.Deactivate(id));
        }
    }
}

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
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<StationListingDTO>))]
        [HttpGet]
        public async Task<IActionResult> GetStationList([FromQuery] StationPageRequest pageRequest)
        {
            return Ok(await _stationService.GetStationsWithPaging(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StationDTO))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStationDetails([FromRoute] int id)
        {
            return Ok(await _stationService.GetStationDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StationDTO))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StationInputDTO stationInputDTO)
        {
            return CreatedAtAction(null, await _stationService.Create(stationInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StationDTO))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StationInputDTO stationInputDTO)
        {
            return Ok(await _stationService.Update(id, stationInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _stationService.ChangeStatus(id, status));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _stationService.Deactivate(id));
        }
    }
}

using FBus_BE.DTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;
using FBus_BE.DTOs.ListingDTOs;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<DriverListingDTO>))]
        [HttpGet]
        public async Task<IActionResult> GetDriverList([FromQuery] DriverPageRequest pageRequest)
        {
            return Ok(await _driverService.GetDriversWithPaging(pageRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverDTO))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDriverDetails([FromRoute] int id)
        {
            return Ok(await _driverService.GetDriverDetails(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DriverDTO))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DriverInputDTO driverInputDTO)
        {
            return CreatedAtAction(null, await _driverService.Create(driverInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverDTO))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DriverInputDTO driverInputDTO)
        {
            return Ok(await _driverService.Update(id, driverInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _driverService.ChangeStatus(id, status));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            return Ok(await _driverService.Delete(id));
        }
    }
}
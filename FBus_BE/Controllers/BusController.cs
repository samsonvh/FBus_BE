using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Models;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetBusList([FromQuery] BusPageRequest pageRequest)
        {
            return Ok(await _busService.GetBusList(pageRequest));
        }

        [Authorize("AdminOnly")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusDetails([FromRoute] int id)
        {
            return Ok(await _busService.GetBusDetails(id));
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BusInputDTO busInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _busService.Create(userId, busInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] BusInputDTO busInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _busService.Update(userId, busInputDTO, id));
        }
    }
}
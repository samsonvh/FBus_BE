using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinationsController : ControllerBase
    {
        private readonly ICoordinationService _coordinationService;

        public CoordinationsController(ICoordinationService coordinationService)
        {
            _coordinationService = coordinationService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCoordinationList([FromQuery] CoordinationPageRequest pageRequest)
        {
            return Ok(await _coordinationService.GetCoordinationList(pageRequest));
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CoordinationInputDTO coordinationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _coordinationService.Create(userId, coordinationInputDTO));
        }

        [Authorize("AdminOnly")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CoordinationInputDTO coordinationInputDTO)
        {
            string user = User.FindFirst("Id").Value;
            int userId = Convert.ToInt32(user);
            return Ok(await _coordinationService.Update(userId, coordinationInputDTO, id));
        }
    }
}

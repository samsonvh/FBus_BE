using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinationsController : ControllerBase
    {
        private readonly ICoordinationService _coordinationService;
        public CoordinationsController(ICoordinationService coordinationService)
        {
            _coordinationService = coordinationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoordinationDTO))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCoordinationsById(int id)
        {
            return Ok(await _coordinationService.GetCoordinationById(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CoordinationDTO))]
        [HttpPost]
        public async Task<IActionResult> CreateNewCoordination([FromBody] CoordinationInputDTO coordinationInputDTO)
        {
            return CreatedAtAction(null, await _coordinationService.Create(coordinationInputDTO));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            return Ok(await _coordinationService.Activate(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _coordinationService.Deactivate(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CoordinationInputDTO coordinationInputDTO)
        {
            return Ok(await _coordinationService.Update(id, coordinationInputDTO));
        }
    }
}
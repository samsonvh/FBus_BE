using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.Dto;
using FBus_BE.Models;
using FBus_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;
        public DriverController(IDriverService driverService, IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDriver()
        {
            var drivers = await _driverService.GetAllDriver();
            if (drivers == null)
            {
                return BadRequest();
            }
            var driverDto = _mapper.Map<IEnumerable<DriverDto>>(drivers);
            return Ok(driverDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDriverById(short id)
        {

            var driver = await _driverService.GetDriverById(id);
            if (driver == null)
            {
                return NotFound();
            }
            var driverDto = _mapper.Map<DriverDto>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewDriver([FromBody] DriverDto driverDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (driverDto == null)
            {
                return BadRequest("Invalid Data");
            }
            var newDriver = _mapper.Map<Driver>(driverDto);
            await _driverService.Create(newDriver);

            var newDriverDto = new DriverDto
            {
                AccountId = newDriver.AccountId,
                FullName = newDriver.FullName,
                Gender = newDriver.Gender,
                IdCardNumber = newDriver.IdCardNumber,
                Address = newDriver.Address,
                PhoneNumber = newDriver.PhoneNumber,
                PersonalEmail = newDriver.PersonalEmail,
                DateOfBirth = newDriver.DateOfBirth,
                Avatar = newDriver.Avatar,
                Status = newDriver.Status,
            };
            return CreatedAtAction(nameof(GetDriverById), new { id = newDriverDto.Id }, newDriverDto);

        }

        [HttpPut("activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActivateDriver(short id)
        {
            bool active = await _driverService.Active(id);
            if (!active)
            {
                return BadRequest();
            }
            return Ok(active);
        }

        [HttpPut("deactive/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeactivateDriver(short id)
        {
            bool deactive = await _driverService.Deactive(id);
            if (!deactive)
            {
                return BadRequest();
            }
            return Ok(deactive);
        }
    }
}
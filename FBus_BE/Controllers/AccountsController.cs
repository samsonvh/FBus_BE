using FBus_BE.DTOs.PageRequests;
using FBus_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBus_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[Authorize(Policy = "AdminOnly")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAccountList([FromQuery] AccountPageRequest pageRequest)
        {
            return Ok(await _accountService.GetAccountList(pageRequest));
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountDetail([FromRoute] int id)
        {
            return Ok(await _accountService.GetAccountDetail(id));
        }
    }
}

using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            ServiceResponse<EmptyDto> response = await _authService.RegisterUserAsync(registerDto);
            return StatusCode(response.Status, response);
        }
    }
}

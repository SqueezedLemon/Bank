using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Bank.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            ServiceResponse<EmptyDto> response = await _authService.RegisterUserAsync(registerDto);
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            Tuple<ServiceResponse<TokenDto>,RefreshToken> response = await _authService.LoginAsync(_configuration, loginDto);
            SetRefreshToken(response.Item2);
            return StatusCode(response.Item1.Status, response.Item1);
        }

        [HttpGet("GetNewToken")]
        public async Task<IActionResult> GetNewToken()
        {
            string refreshToken = Request.Cookies["refreshToken"];
            Tuple<ServiceResponse<TokenDto>, RefreshToken> response = await _authService.GetNewTokenAsync(_configuration, refreshToken);
            SetRefreshToken(response.Item2);
            return StatusCode(response.Item1.Status, response.Item1);
        }


        private void SetRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.ExpiresOn,
                };
                Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            }
        }
    }
}

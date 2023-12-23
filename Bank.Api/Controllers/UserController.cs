using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Bank.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.LogoutAsync(userEmail);
            return StatusCode(response.Status, response);
        }

        [HttpGet("CurrentBalance")]
        public async Task<IActionResult> CurrentBalance()
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<BalanceDto> response = await _userService.CheckCurrentBalanceAsync(userEmail);
            return StatusCode(response.Status, response);
        }

        [HttpPost("TransactionHistory")]
        public async Task<IActionResult> TransactionHistory([FromBody] TransactionHistoryDateDto transactionHistoryDateDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<List<TransactionHistoryDto>> response = await _userService.GetTransactionHistoryAsync(userEmail, transactionHistoryDateDto.FromDate);
            return StatusCode(response.Status, response);
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto transactionDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.DepositAsync(userEmail, transactionDto);
            return StatusCode(response.Status, response);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto transactionDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.WithdrawAsync(userEmail, transactionDto);
            return StatusCode(response.Status, response);
        }
    }
}

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
    /// <summary>
    /// Controller that handles user actions: Withdraw, Deposit, BalanceCheck and TransactionHistory.
    /// Must have access token as bearer token and role "User".
    /// </summary>
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

        /// <summary>
        /// Method to revokes user's refresh token.
        /// </summary>
        /// <returns> StatusCode(code, ServiceResponse<EmptyDto>) </returns>
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.LogoutAsync(userEmail);
            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Method to check current balance of user
        /// </summary>
        /// <returns> StatusCode(code, ServiceResponse<BalanceDto>)  </returns>
        [HttpGet("CurrentBalance")]
        public async Task<IActionResult> CurrentBalance()
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<BalanceDto> response = await _userService.CheckCurrentBalanceAsync(userEmail);
            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Method to get all transaction history after a said date.
        /// </summary>
        /// <param name="transactionHistoryDateDto"> TransactionHistoryDateDto </param>
        /// <returns> StatusCode(code, ServiceResponse<List<TransactionHistoryDto>>) </returns>
        [HttpPost("TransactionHistory")]
        public async Task<IActionResult> TransactionHistory([FromBody] TransactionHistoryDateDto transactionHistoryDateDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<List<TransactionHistoryDto>> response = await _userService.GetTransactionHistoryAsync(userEmail, transactionHistoryDateDto.FromDate);
            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Method to Deposit said amount to user's account.
        /// </summary>
        /// <param name="transactionDto"> TransactionDto </param>
        /// <returns> StatusCode(code, ServiceResponse<EmptyDto>) </returns>
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto transactionDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.DepositAsync(userEmail, transactionDto);
            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Method to withdraw said amount from user's account.
        /// </summary>
        /// <param name="transactionDto"> TransactionDto </param>
        /// <returns> StatusCode(code, ServiceResponse<EmptyDto>) </returns>
        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto transactionDto)
        {
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            ServiceResponse<EmptyDto> response = await _userService.WithdrawAsync(userEmail, transactionDto);
            return StatusCode(response.Status, response);
        }
    }
}

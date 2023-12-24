using Bank.Application.Contracts.Repositories;
using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Bank.Domain;
using Microsoft.AspNetCore.Identity;

namespace Bank.Service
{
    /// <summary>
    /// Services provided for user controller.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseService _responseService;
        private readonly ITokenService _tokenService;
        private readonly IBalanceService _balanceService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IBalanceRepo _balanceRepo;
        private readonly ITransactionHistoryRepo _transactionHistoryRepo;
        public UserService(UserManager<ApplicationUser> userManager, IResponseService responseService, ITokenService tokenService, IBalanceService balanceService, ITransactionHistoryService transactionHistoryService, IBalanceRepo balanceRepo, ITransactionHistoryRepo transactionHistoryRepo)
        {
            _userManager = userManager;
            _responseService = responseService;
            _tokenService = tokenService;
            _balanceService = balanceService;
            _transactionHistoryService = transactionHistoryService;
            _balanceRepo = balanceRepo;
            _transactionHistoryRepo = transactionHistoryRepo;
        }

        /// <summary>
        /// Method to logout user/revoke refresh token.
        /// </summary>
        /// <param name="email"> string </param>
        /// <returns> ServiceResponse<EmptyDto> </returns>
        public async Task<ServiceResponse<EmptyDto>> LogoutAsync(string email)
        {
            EmptyDto emptyDto = new();
            var currentUser = await _userManager.FindByEmailAsync(email);
            try 
            { 
                if (currentUser.isDisabled) 
                {
                    return _responseService.GenerateResponse(emptyDto, false, "User Disabled!", 400);
                }
                await _tokenService.RevokeRefreshToken(currentUser.Id);
                return _responseService.GenerateResponse(emptyDto, true, "User successfully logged out!", 200);
            }
            catch (Exception ex) 
            {
                return _responseService.GenerateResponse(emptyDto, false, ex.Message, 400);
            }
        }

        /// <summary>
        /// Method to deposit amount in user's account and create transaction history.
        /// </summary>
        /// <param name="email"> string </param>
        /// <param name="transactionDto"> TransactionDto </param>
        /// <returns> ServiceResponse<EmptyDto> </returns>
        public async Task<ServiceResponse<EmptyDto>> DepositAsync(string email, TransactionDto transactionDto)
        {
            EmptyDto emptyDto = new();
            var currentUser = await _userManager.FindByEmailAsync(email);
            try
            {
                if (currentUser.isDisabled)
                {
                    return _responseService.GenerateResponse(emptyDto, false, "User Disabled!", 400);
                }
                if (transactionDto != null)
                {
                    if (await _balanceService.AddBalanceAsync(currentUser.Id, transactionDto.Amount))
                    {
                        await _transactionHistoryService.CreateTransactionHistory("Deposit:"+transactionDto.TransactionDetails, transactionDto.Amount, currentUser.Id);
                        return _responseService.GenerateResponse(emptyDto, true, "Transaction Completed!", 200);
                    }
                    return _responseService.GenerateResponse(emptyDto, false, "Transaction Failed: Could not add balance", 400);
                }
                return _responseService.GenerateResponse(emptyDto, false, "Transaction Failed: Invalid Details", 400);
            }
            catch (Exception ex)
            {
                return _responseService.GenerateResponse(emptyDto, false, ex.Message, 400);
            }
        }

        /// <summary>
        /// Method to withdraw amount from user's account and create transaction history.
        /// Generates 400 badrequest if withdraw amount is greater than bank balance.
        /// </summary>
        /// <param name="email"> string </param>
        /// <param name="transactionDto"> TransactionDto </param>
        /// <returns> ServiceResponse<EmptyDto> </returns>
        public async Task<ServiceResponse<EmptyDto>> WithdrawAsync(string email, TransactionDto transactionDto)
        {
            EmptyDto emptyDto = new();
            var currentUser = await _userManager.FindByEmailAsync(email);
            try
            {
                if (currentUser.isDisabled)
                {
                    return _responseService.GenerateResponse(emptyDto, false, "User Disabled!", 400);
                }
                if (transactionDto != null)
                {
                    if (await _balanceService.RemoveBalanceAsync(currentUser.Id, transactionDto.Amount))
                    {
                        await _transactionHistoryService.CreateTransactionHistory("Withdraw:" + transactionDto.TransactionDetails, transactionDto.Amount, currentUser.Id);
                        return _responseService.GenerateResponse(emptyDto, true, "Transaction Completed!", 200);
                    }
                    return _responseService.GenerateResponse(emptyDto, false, "Transaction Failed: Insufficient Balance!", 400);
                }
                return _responseService.GenerateResponse(emptyDto, false, "Transaction Failed: Invalid Details", 400);
            }
            catch (Exception ex)
            {
                return _responseService.GenerateResponse(emptyDto, false, ex.Message, 400);
            }
        }

        /// <summary>
        /// Method to check current balance of user.
        /// </summary>
        /// <param name="email"> string </param>
        /// <returns> ServiceResponse<BalanceDto> </returns>
        public async Task<ServiceResponse<BalanceDto>> CheckCurrentBalanceAsync(string email)
        {
            BalanceDto balanceDto = new();
            var currentUser = await _userManager.FindByEmailAsync(email);
            try
            {
                if (currentUser.isDisabled)
                {
                    balanceDto = null;
                    return _responseService.GenerateResponse(balanceDto, false, "User Disabled!", 400);
                }
                Balance balance = await _balanceRepo.GetBalanceObjectOfUser(currentUser.Id);
                balanceDto.Balance = balance.BankBalance;
                return _responseService.GenerateResponse(balanceDto, true, "Request Successful!", 200);
            }
            catch (Exception ex)
            {
                balanceDto = null;
                return _responseService.GenerateResponse(balanceDto, false, ex.Message, 400);
            }
        }

        /// <summary>
        /// Method to get all transaction histories after a certain date.
        /// </summary>
        /// <param name="email"> string </param>
        /// <param name="fromDate"> DateTime </param>
        /// <returns> ServiceResponse<List<TransactionHistoryDto>> </returns>
        public async Task<ServiceResponse<List<TransactionHistoryDto>>> GetTransactionHistoryAsync(string email, DateTime fromDate)
        {
            List<TransactionHistoryDto> transactionHistoryDtos = new();
            var currentUser = await _userManager.FindByEmailAsync(email);
            try
            {
                if (currentUser.isDisabled)
                {
                    transactionHistoryDtos = null;
                    return _responseService.GenerateResponse(transactionHistoryDtos, false, "User Disabled!", 400);
                }
                List<TransactionHistory> transactionHistories = await _transactionHistoryRepo.GetTransactionHistoriesAsync(currentUser.Id, fromDate);
                transactionHistoryDtos = _transactionHistoryService.MapTransactionHistoryListToTransactionHistoryDtoList(transactionHistories);
                return _responseService.GenerateResponse(transactionHistoryDtos, true, "Request Successful!", 200);
            }
            catch (Exception ex)
            {
                transactionHistoryDtos = null;
                return _responseService.GenerateResponse(transactionHistoryDtos, false, ex.Message, 400);
            }
        }
    }
}

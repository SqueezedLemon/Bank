using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<EmptyDto>> LogoutAsync(string email);
        Task<ServiceResponse<EmptyDto>> DepositAsync(string email, TransactionDto transactionDto);
        Task<ServiceResponse<EmptyDto>> WithdrawAsync(string email, TransactionDto transactionDto);
        Task<ServiceResponse<BalanceDto>> CheckCurrentBalanceAsync(string email);
        Task<ServiceResponse<List<TransactionHistoryDto>>> GetTransactionHistoryAsync(string email, DateTime fromDate);
    }
}

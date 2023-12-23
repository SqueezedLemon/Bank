using Bank.Application.DTOs;
using Bank.Domain;

namespace Bank.Application.Contracts.Services
{
    public interface ITransactionHistoryService
    {
        Task<bool> CreateTransactionHistory(string transactionDetails, decimal transactionAmount, string userId);
        List<TransactionHistoryDto> MapTransactionHistoryListToTransactionHistoryDtoList(List<TransactionHistory> transactionHistory);
    }
}

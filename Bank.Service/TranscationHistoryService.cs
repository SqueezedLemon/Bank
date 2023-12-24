using Bank.Application.Contracts.Repositories;
using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Domain;

namespace Bank.Service
{
    /// <summary>
    /// Services related to transaction history.
    /// </summary>
    public class TranscationHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepo _transactionHistoryRepo;
        public TranscationHistoryService(ITransactionHistoryRepo transactionHistoryRepo)
        {
            _transactionHistoryRepo = transactionHistoryRepo;
        }

        /// <summary>
        /// Method to add new transaction history in db.
        /// </summary>
        /// <param name="transactionDetails"> string </param>
        /// <param name="transactionAmount"> decimal </param>
        /// <param name="userId"> string </param>
        /// <returns> bool </returns>
        public async Task<bool> CreateTransactionHistory(string transactionDetails, decimal transactionAmount, string userId)
        {
            try
            {
                await _transactionHistoryRepo.Add(CreateTransactionHistoryObject(transactionDetails, transactionAmount, userId));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Method to map list of TransactionHistory to list of TransactionHistoryDto.
        /// </summary>
        /// <param name="transactionHistory"> List<TransactionHistory> </param>
        /// <returns> List<TransactionHistoryDto> </returns>
        public List<TransactionHistoryDto> MapTransactionHistoryListToTransactionHistoryDtoList(List<TransactionHistory> transactionHistory)
        {
            List<TransactionHistoryDto> transactionHistoryDtos = new();
            foreach(TransactionHistory transaction in transactionHistory)
            {
                TransactionHistoryDto transactionHistoryDto = new()
                { 
                    TransactionDetails = transaction.TransactionDetails,
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDateTime = transaction.TransactionDateTime,
                };
                transactionHistoryDtos.Add(transactionHistoryDto);
            }
            return transactionHistoryDtos;
        }

        /// <summary>
        /// Private method to create a TransactionHistory object for current time from given parameters.
        /// </summary>
        /// <param name="transactionDetails"> string </param>
        /// <param name="transactionAmount"> decimal </param>
        /// <param name="userId"> string </param>
        /// <returns></returns>
        private TransactionHistory CreateTransactionHistoryObject(string transactionDetails, decimal transactionAmount, string userId) 
        {
            TransactionHistory transactionHistory = new() 
            {
                TransactionDetails = transactionDetails,
                TransactionAmount = transactionAmount,
                UserId = userId,
                TransactionDateTime = DateTime.Now,
            };
            return transactionHistory;
        }
    }
}

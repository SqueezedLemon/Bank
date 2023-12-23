using Bank.Application.Contracts.Repositories;
using Bank.Application.Contracts.Services;
using Bank.Application.DTOs;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class TranscationHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepo _transactionHistoryRepo;
        public TranscationHistoryService(ITransactionHistoryRepo transactionHistoryRepo)
        {
            _transactionHistoryRepo = transactionHistoryRepo;
        }
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

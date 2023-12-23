using Bank.Application.Contracts.Repositories;
using Bank.Application.Contracts.Services;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepo _balanceRepo;
        public BalanceService(IBalanceRepo balanceRepo)
        {
            _balanceRepo = balanceRepo;
        }

        public async Task<bool> AddBalanceAsync(string userId, decimal amount)
        {
            try
            {
                Balance balance = await _balanceRepo.GetBalanceObjectOfUser(userId);
                if (balance == null)
                {
                    return false;
                }
                balance.BankBalance += amount;
                await _balanceRepo.Update(balance);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveBalanceAsync(string userId, decimal amount)
        {
            try
            {
                Balance balance = await _balanceRepo.GetBalanceObjectOfUser(userId);
                if (balance == null)
                {
                    return false;
                }
                if (balance.BankBalance >= amount)
                {
                    balance.BankBalance -= amount;
                    await _balanceRepo.Update(balance);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetBalanceAsync(string userId, decimal amount)
        {
            try 
            {
                Balance balance = await _balanceRepo.GetBalanceObjectOfUser(userId);
                if (balance == null)
                {
                    await _balanceRepo.Add(CreateBalanceObject(userId, amount));
                    return true;
                }
                balance.BankBalance = amount;
                await _balanceRepo.Update(balance);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private Balance CreateBalanceObject(string userId, decimal amount) 
        {
            Balance balance = new()
            {
                BankBalance = amount,
                UserId = userId
            };
            return balance;
        }
    }
}

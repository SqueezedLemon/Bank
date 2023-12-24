using Bank.Application.Contracts.Repositories;
using Bank.Application.Contracts.Services;
using Bank.Domain;

namespace Bank.Service
{
    /// <summary>
    /// Services related to balance.
    /// </summary>
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepo _balanceRepo;
        public BalanceService(IBalanceRepo balanceRepo)
        {
            _balanceRepo = balanceRepo;
        }

        /// <summary>
        /// Method to add balance to user's account.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="amount"> decimal </param>
        /// <returns> bool </returns>
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

        /// <summary>
        /// Method to reduce balance from user account
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="amount"> string </param>
        /// <returns> bool </returns>
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

        /// <summary>
        /// Method to set balance of user account to a specific amount.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="amount"> decimal </param>
        /// <returns> bool </returns>
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

        /// <summary>
        /// Private Method to create Balance object from given parameters.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="amount"> decimal </param>
        /// <returns> Balance </returns>
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

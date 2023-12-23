using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Services
{
    public interface IBalanceService
    {
        Task<bool> SetBalanceAsync(string userId, decimal amount);
        Task<bool> AddBalanceAsync(string userId, decimal amount);
        Task<bool> RemoveBalanceAsync(string userId, decimal amount);

    }
}

using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Repositories
{
    public interface ITransactionHistoryRepo : IGenericRepo<TransactionHistory>
    {
        Task<List<TransactionHistory>> GetTransactionHistoriesAsync(string userId, DateTime fromDate);
    }
}

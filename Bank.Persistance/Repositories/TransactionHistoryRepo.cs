using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistance.Repositories
{
    /// <summary>
    /// Class that performs CRUD operations on TransactionHistory table.
    /// </summary>
    public class TransactionHistoryRepo : GenericRepo<TransactionHistory>, ITransactionHistoryRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionHistoryRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to get transaction history of a user from db.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="fromDate"> string </param>
        /// <returns></returns>
        public Task<List<TransactionHistory>> GetTransactionHistoriesAsync(string userId, DateTime fromDate)
        {
            return _dbContext.TransactionHistories.Where(th => th.UserId == userId && th.TransactionDateTime > fromDate).ToListAsync();
        }
    }
}

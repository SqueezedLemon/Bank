using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistance.Repositories
{
    /// <summary>
    /// Class that performs CRUD operation on Balance table.
    /// </summary>
    public class BalanceRepo : GenericRepo<Balance> , IBalanceRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public BalanceRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to get Balance object from database
        /// </summary>
        /// <param name="userId"> string </param>
        /// <returns> Balance </returns>
        public Task<Balance> GetBalanceObjectOfUser(string userId)
        {
            return _dbContext.Balances.FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }
}

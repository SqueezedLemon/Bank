using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Persistance.Repositories
{
    public class TransactionHistoryRepo : GenericRepo<TransactionHistory>, ITransactionHistoryRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionHistoryRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<TransactionHistory>> GetTransactionHistoriesAsync(string userId, DateTime fromDate)
        {
            return _dbContext.TransactionHistories.Where(th => th.UserId == userId && th.TransactionDateTime > fromDate).ToListAsync();
        }
    }
}

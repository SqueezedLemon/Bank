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
    public class BalanceRepo : GenericRepo<Balance> , IBalanceRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public BalanceRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Balance> GetBalanceObjectOfUser(string userId)
        {
            return _dbContext.Balances.FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }
}

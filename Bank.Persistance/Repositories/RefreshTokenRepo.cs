using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistance.Repositories
{
    public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshTokenRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DateTime> GetExpiryDateAsync(string refreshToken)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return token.ExpiresOn;
        }

        public async Task<bool> IsRevoked(string refreshToken)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return token.IsRevoked;
        }

        public async Task RevokeRefreshTokenAsync(string userId)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            token.IsRevoked = true;
            _dbContext.Update(token);
            _dbContext.SaveChanges();
        }

        public async Task<bool> TokenExistsForUser(string userId)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            return token != null;
        }

        public async Task<RefreshToken> GetByUserIdAsync(string userId)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
        }

        public async Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        }
    }
}

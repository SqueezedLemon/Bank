using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistance.Repositories
{
    /// <summary>
    /// Class that handles CRUD operations on RefreshToken table.
    /// </summary>
    public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshTokenRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to get expiry date from db of given refresh token.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <returns> DateTime </returns>
        public async Task<DateTime> GetExpiryDateAsync(string refreshToken)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return token.ExpiresOn;
        }

        /// <summary>
        /// Method to get IsRevoked status of given refresh token.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <returns> bool </returns>
        public async Task<bool> IsRevoked(string refreshToken)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return token.IsRevoked;
        }

        /// <summary>
        /// Method to set IsRevoked true in db of given refresh token.
        /// </summary>
        /// <param name="userId"> string </param>
        public async Task RevokeRefreshTokenAsync(string userId)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            token.IsRevoked = true;
            _dbContext.Update(token);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that checks if refresh token entity exists for given user in db.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <returns> bool </returns>
        public async Task<bool> TokenExistsForUser(string userId)
        {
            RefreshToken token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
            return token != null;
        }

        /// <summary>
        /// Method to get RefreshToken object of given user .
        /// </summary>
        /// <param name="userId"> string </param>
        /// <returns> RefreshToken </returns>
        public async Task<RefreshToken> GetByUserIdAsync(string userId)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
        }

        /// <summary>
        /// Method to get RefreshToken object from given refreshToken string.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <returns> RefreshToken </returns>
        public async Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        }
    }
}
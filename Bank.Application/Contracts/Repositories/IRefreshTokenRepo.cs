using Bank.Domain;

namespace Bank.Application.Contracts.Repositories
{
    public interface IRefreshTokenRepo : IGenericRepo<RefreshToken>
    {
        Task<bool> IsRevoked(string refreshToken);
        Task RevokeRefreshTokenAsync(string userId);
        Task<DateTime> GetExpiryDateAsync(string refreshToken);
        Task<bool> TokenExistsForUser(string userId);
        Task<RefreshToken> GetByUserIdAsync(string userId);
        Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken);
    }
}

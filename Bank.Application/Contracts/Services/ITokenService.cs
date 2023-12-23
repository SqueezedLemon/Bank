using Bank.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Services
{
    public interface ITokenService
    {
        string GenerateJwtTokenString(IConfiguration configuration,string email, string role);
        string GenerateRefreshToken();
        Task<bool> IsRefreshTokenRevoked(string refreshToken);
        Task<bool> IsRefreshTokenExpired(string refreshToken, DateTime dateTime);
        Task<RefreshToken> AddRefreshToken(string userId, string refreshToken);
        Task RevokeRefreshToken(string userId);
        Task<ApplicationUser> GetUserFromRefreshToken(string refreshToken);
    }
}

using Bank.Application.Contracts.Services;
using Bank.Domain;
using System.Text;
using Microsoft.Extensions.Configuration;
using Bank.Application.Contracts.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Bank.Service
{
    /// <summary>
    /// Services for token generation and token storage.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public TokenService(IRefreshTokenRepo refreshTokenRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
        }

        /// <summary>
        /// Method to generate JWT token for authorization.
        /// </summary>
        /// <param name="configuration"> IConfiguration </param>
        /// <param name="email"> string </param>
        /// <param name="role"> string </param>
        /// <returns> string </returns>
        public string GenerateJwtTokenString(IConfiguration configuration, string email, string role)
        {
            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
            };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken securityToken = new(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        /// <summary>
        /// Method to generate refresh token.
        /// </summary>
        /// <returns> string </returns>
        public string GenerateRefreshToken()
        {
            byte[] randomBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            Guid guid = new(randomBytes);
            return guid.ToString();
        }

        /// <summary>
        /// Method to add refresh token of user to db.
        /// </summary>
        /// <param name="userId"> string </param>
        /// <param name="refreshToken"> string </param>
        /// <returns> RefreshToken </returns>
        public async Task<RefreshToken> AddRefreshToken(string userId, string refreshToken)
        {
            if (await _refreshTokenRepo.TokenExistsForUser(userId))
            {
                RefreshToken token = await _refreshTokenRepo.GetByUserIdAsync(userId);
                token.Token = refreshToken;
                token.CreatedOn = DateTime.Now;
                token.ExpiresOn = DateTime.Now.AddDays(7);
                token.IsRevoked = false;
                await _refreshTokenRepo.Update(token);
                return token;
            }
            else
            {
                RefreshToken token = new()
                {
                    Token = refreshToken,
                    UserId = userId,
                    CreatedOn = DateTime.Now,
                    ExpiresOn = DateTime.Now.AddDays(7)
                };
                await _refreshTokenRepo.Add(token);
                return token;
            }
        }

        /// <summary>
        /// Method to find the associated user from given refresh token.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <returns> ApplicationUser </returns>
        public async Task<ApplicationUser> GetUserFromRefreshToken(string refreshToken)
        {
            RefreshToken token = await _refreshTokenRepo.GetByRefreshTokenAsync(refreshToken);
            return token.User;
        }

        /// <summary>
        /// Method to check if given refresh token has expired.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <param name="dateTime"> DateTime </param>
        /// <returns> bool </returns>
        public async Task<bool> IsRefreshTokenExpired(string refreshToken, DateTime dateTime)
        {
            DateTime expiry = await _refreshTokenRepo.GetExpiryDateAsync(refreshToken);
            return expiry < dateTime;
        }


        /// <summary>
        /// Method to check if given refresh token has been revoked.
        /// </summary>
        /// <param name="refreshToken"> string </param>
        /// <returns> bool </returns>
        public async Task<bool> IsRefreshTokenRevoked(string refreshToken)
        {
            return await _refreshTokenRepo.IsRevoked(refreshToken);
        }

        /// <summary>
        /// Method that revokes the refresh token of given user.
        /// </summary>
        /// <param name="userId"> string </param>
        public async Task RevokeRefreshToken(string userId)
        {
            await _refreshTokenRepo.RevokeRefreshTokenAsync(userId);
        }
    }
}

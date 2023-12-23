using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;
using Bank.Domain;
using Microsoft.Extensions.Configuration;

namespace Bank.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<EmptyDto>> RegisterUserAsync(RegisterDto registerDto);
        Task<Tuple<ServiceResponse<TokenDto>,RefreshToken>> LoginAsync(IConfiguration configuration,LoginDto loginDto);
        Task<Tuple<ServiceResponse<TokenDto>, RefreshToken>> GetNewTokenAsync(IConfiguration configuration, string refreshToken);
    }
}

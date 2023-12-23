using Bank.Application.DTOs;
using Bank.Application.ServiceResponse;

namespace Bank.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<EmptyDto>> RegisterUserAsync(RegisterDto registerDto);
    }
}

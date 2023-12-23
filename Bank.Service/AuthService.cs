using Bank.Application.DTOs;
using Bank.Domain;
using Bank.Application.ServiceResponse;
using Microsoft.AspNetCore.Identity;
using Bank.Application.Contracts.Services;

namespace Bank.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseService _responseService;

        public AuthService(UserManager<ApplicationUser> userManager, IResponseService responseService)
        {
            _userManager = userManager;
            _responseService = responseService;
        }

        public async Task<ServiceResponse<EmptyDto>> RegisterUserAsync(RegisterDto registerDto)
        {
            EmptyDto emptyDto = new();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerDto.Email,
                        Email = registerDto.Email,
                    };
                    var result = await _userManager.CreateAsync(user, registerDto.Password);
                    if (result.Succeeded)
                    {
                        return _responseService.GenerateResponse(emptyDto, true, "New User Created!", 200);
                    }
                    return _responseService.GenerateResponse(emptyDto, false, "Cannot Create New User!", 400);
                }
                return _responseService.GenerateResponse(emptyDto, false, "User Exists!", 400);
            }
            catch (Exception ex)
            {
                return _responseService.GenerateResponse(emptyDto, false, ex.Message, 400);
            }
        }
    }
}

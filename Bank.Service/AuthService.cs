using Bank.Application.DTOs;
using Bank.Domain;
using Bank.Application.ServiceResponse;
using Microsoft.AspNetCore.Identity;
using Bank.Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Bank.Persistance.Constants;
using Bank.Application.Contracts.Repositories;

namespace Bank.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseService _responseService;
        private readonly ITokenService _tokenService;
        private readonly IUserDetailService _userDetailService;
        private readonly IBalanceService _balanceService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IUserDetailRepo _userDetailRepo;

        public AuthService(UserManager<ApplicationUser> userManager, IResponseService responseService, ITokenService tokenService, IUserDetailService userDetailService, IBalanceService balanceService, ITransactionHistoryService transactionHistoryService, IUserDetailRepo userDetailRepo)
        {
            _userManager = userManager;
            _responseService = responseService;
            _tokenService = tokenService;
            _userDetailService = userDetailService;
            _balanceService = balanceService;
            _transactionHistoryService = transactionHistoryService;
            _userDetailRepo = userDetailRepo;
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
                        UserDetail userDetail = _userDetailService.CreateUserDetailObject(registerDto.Name, registerDto.CitizenshipNumber, registerDto.Dob, user.Id);
                        await _userDetailRepo.Add(userDetail);
                        await _balanceService.SetBalanceAsync(user.Id, 0);
                        await _transactionHistoryService.CreateTransactionHistory("Bank account created with balance 0.", 0, user.Id);
                        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
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

        public async Task<Tuple<ServiceResponse<TokenDto>, RefreshToken>> LoginAsync(IConfiguration configuration, LoginDto loginDto)
        {
            TokenDto tokenDto = new();
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(loginDto.Email);
                var role = await _userManager.GetRolesAsync(identityUser);
                if (identityUser == null)
                {
                    tokenDto = null;
                    return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, false, "User not found!", 400), null);
                }
                if (await _userManager.CheckPasswordAsync(identityUser, loginDto.Password))
                {
                    tokenDto.JwtToken = _tokenService.GenerateJwtTokenString(configuration, loginDto.Email, role[0].ToString());
                    tokenDto.ExpiresOn = DateTime.Now.AddMinutes(15);
                    string newRefreshToken = _tokenService.GenerateRefreshToken();
                    RefreshToken refreshTokenObject = await _tokenService.AddRefreshToken(identityUser.Id, newRefreshToken);
                    return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, true, "Login Successful!", 200), refreshTokenObject);
                }
                tokenDto = null;
                return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, false, "InvalidCredentials!", 400), null);
            }
            catch (Exception ex)
            {
                tokenDto = null;
                return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, false, ex.Message, 400), null);
            }
        }

        public async Task<Tuple<ServiceResponse<TokenDto>, RefreshToken>> GetNewTokenAsync(IConfiguration configuration, string refreshToken)
        {
            TokenDto tokenDto = new();
            try
            {
                if (!await _tokenService.IsRefreshTokenRevoked(refreshToken) && !await _tokenService.IsRefreshTokenExpired(refreshToken, DateTime.Now))
                {
                    ApplicationUser user = await _tokenService.GetUserFromRefreshToken(refreshToken);
                    var role = await _userManager.GetRolesAsync(user);
                    tokenDto.JwtToken = _tokenService.GenerateJwtTokenString(configuration, user.Email, role[0].ToString());
                    tokenDto.ExpiresOn = DateTime.Now.AddMinutes(15);
                    string newRefreshToken = _tokenService.GenerateRefreshToken();
                    RefreshToken refreshTokenObject = await _tokenService.AddRefreshToken(user.Id, newRefreshToken);
                    return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, true, "Login Successful!", 200), refreshTokenObject);
                }
                tokenDto = null;
                return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, false, "Refresh Token Revoked or Expired. Login Again!!!", 400), null);
            }
            catch (Exception ex)
            {
                tokenDto = null;
                return new Tuple<ServiceResponse<TokenDto>, RefreshToken>(_responseService.GenerateResponse(tokenDto, false, ex.Message, 400), null);
            }
        }
    }
}

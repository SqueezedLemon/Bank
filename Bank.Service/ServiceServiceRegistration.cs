using Bank.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Service
{
    public static class ServiceServiceRegistration
    {
        public static IServiceCollection ConfigureServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<ITransactionHistoryService, TranscationHistoryService>();

            return services;
        }
    }
}

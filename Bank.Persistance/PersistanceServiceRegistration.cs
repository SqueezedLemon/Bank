using Bank.Application.Contracts.Repositories;
using Bank.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
            services.AddScoped<IUserDetailRepo, UserDetailRepo>();
            services.AddScoped<IBalanceRepo, BalanceRepo>();
            services.AddScoped<ITransactionHistoryRepo, TransactionHistoryRepo>();

            return services;
        }
    }
}

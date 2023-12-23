using Bank.Persistance.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistance
{
    public static class DbSeeder
    {
        public static async Task Seeder(IServiceProvider service)
        {
            //Seed Roles
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}

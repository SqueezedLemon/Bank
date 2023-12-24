using Bank.Persistance.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistance
{
    /// <summary>
    /// Seed constants in db.
    /// </summary>
    public static class DbSeeder
    {
        /// <summary>
        /// Method to seed constants in db.
        /// </summary>
        /// <param name="service"> IServiceProvider </param>
        public static async Task Seeder(IServiceProvider service)
        {
            //Seed Roles
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}

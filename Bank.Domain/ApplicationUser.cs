using Microsoft.AspNetCore.Identity;

namespace Bank.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public bool isDisabled { get; set; } = false;
    }
}

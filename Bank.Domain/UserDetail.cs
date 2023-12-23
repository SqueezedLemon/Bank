using System.ComponentModel.DataAnnotations;

namespace Bank.Domain
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CitizenshipNumber { get; set; }
        public DateOnly Dob { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

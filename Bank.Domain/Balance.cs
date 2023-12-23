using System.ComponentModel.DataAnnotations;

namespace Bank.Domain
{
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public decimal BankBalance { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Bank.Domain
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        public string? TransactionDetails { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

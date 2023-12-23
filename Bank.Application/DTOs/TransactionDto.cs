using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.DTOs
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public string? TransactionDetails { get; set; }
    }
}

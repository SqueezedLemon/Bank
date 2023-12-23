using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.DTOs
{
    public class TokenDto
    {
        public string? JwtToken { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}

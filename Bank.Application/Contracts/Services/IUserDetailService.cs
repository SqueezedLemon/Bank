using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Services
{
    public interface IUserDetailService
    {
        UserDetail CreateUserDetailObject(string Name, string CitizenshipNumber, DateOnly Dob, string UserId);
    }
}

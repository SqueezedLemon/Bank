using Bank.Application.Contracts.Services;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class UserDetailService : IUserDetailService
    {
        public UserDetail CreateUserDetailObject(string Name, string CitizenshipNumber, DateOnly Dob, string UserId)
        {
            UserDetail userDetail = new() 
            {
                Name = Name,
                CitizenshipNumber = CitizenshipNumber,
                Dob = Dob,
                UserId = UserId
            };
            return userDetail;
        }
    }
}

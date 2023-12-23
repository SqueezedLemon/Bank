﻿using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Contracts.Repositories
{
    public interface IBalanceRepo : IGenericRepo<Balance>
    {
        Task<Balance> GetBalanceObjectOfUser(string userId);
    }
}

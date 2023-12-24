﻿using Bank.Application.Contracts.Repositories;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Persistance.Repositories
{
    /// <summary>
    /// Class that performs CRUD operations on UserDetails table.
    /// </summary>
    public class UserDetailRepo : GenericRepo<UserDetail> , IUserDetailRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public UserDetailRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

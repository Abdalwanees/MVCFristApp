﻿using Microsoft.EntityFrameworkCore;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.DAL.Data;
using MVCFristApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        //private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext):base(dbContext){ }

        public IQueryable<Employee> GetByName(string name)
        {
            return _dbContext.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower()));
        }
    }
}

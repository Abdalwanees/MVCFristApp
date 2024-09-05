using Microsoft.EntityFrameworkCore;
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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            var department = _dbContext.Departments.AsNoTracking().ToList();
            return department;
        }

        public Department GetById(int id)
        {
            //var department = _dbContext.Departments.Where(D=>D.Id==id).FirstOrDefault();
            //Best Performance
            var department= _dbContext.Departments.Find(id);
            return department;
           
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}

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
    public class GenericRepository<T> : IGenericrepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(D=>D.workfor).AsNoTracking().ToList(); 

            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetById(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (T)(object)_dbContext.Employees.Include(D => D.workfor).FirstOrDefault(e => e.Id == id);
            }
            else
            {
                return _dbContext.Set<T>().Find(id);
            }
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}

using MVCFristApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.BLL.Interfaces
{
    public interface IGenericrepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);  
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}

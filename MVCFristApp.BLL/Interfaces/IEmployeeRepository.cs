using MVCFristApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericrepository<Employee>
    {
        IQueryable<Employee> GetByAddress(string address);//Filtration   
    }
}

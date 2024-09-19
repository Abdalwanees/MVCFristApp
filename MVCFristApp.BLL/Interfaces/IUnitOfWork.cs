using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable  //to close connection with database
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        int Compelete();
    }
}

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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext dbContext):base(dbContext){}
    }
}

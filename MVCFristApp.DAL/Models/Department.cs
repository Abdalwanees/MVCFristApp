using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.DAL.Models
{
    // Class Represent message on DB
    public class Department : ModelBase
    {
        public string Code { get; set; } 
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        //Navigation property
        public ICollection<Employee> Employees { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.DAL.Models
{
    // Class Represent message on DB
    public class Department
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Code Is Reqired!.")] //-->Put it in View Model
        public string Code { get; set; } //Allow null we use .net (5) not suppot nullable Reference type
        //[Required(ErrorMessage ="Name Is Required!.")] //-->Put it in View Model
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}

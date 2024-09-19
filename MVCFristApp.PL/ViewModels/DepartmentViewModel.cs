using MVCFristApp.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVCFristApp.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Reqired!.")] //-->Put it in View Model
        public string Code { get; set; } //Allow null we use .net (5) not suppot nullable Reference type
        [Required(ErrorMessage = "Name Is Required!.")] //-->Put it in View Model
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        //Navigation property
        public ICollection<Employee> Employees { get; set; }
    }
}

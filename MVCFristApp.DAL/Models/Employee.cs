using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVCFristApp.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }

    public enum EmployeeType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1,
        [EnumMember(Value = "PartTime")]
        PartTime = 2
    }

    public class Employee : ModelBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; } // soft delete

        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }

        public int? workForId { get; set; }
        //navigation property
        public Department workfor { get; set; }
    }
}

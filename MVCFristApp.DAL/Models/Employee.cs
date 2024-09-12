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
        //public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max Length For Name is 50 characters")]
        [MinLength(4, ErrorMessage = "Min Length For Name is 4 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

        // Adjusted the regular expression for address validation
        [RegularExpression(@"^[0-9]{1,5}\s[A-Za-z]+\s[A-Za-z]+(?:\s[A-Za-z]+)*$",
                           ErrorMessage = "Address must be in a valid format like '123 Street Name City'")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; } // soft delete

        public Gender Gender { get; set; }
    }
}

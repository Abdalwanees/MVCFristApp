using System.ComponentModel.DataAnnotations;

namespace MVCFristApp.PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

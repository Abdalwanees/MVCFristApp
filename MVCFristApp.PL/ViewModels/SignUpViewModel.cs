using System.ComponentModel.DataAnnotations;

namespace MVCFristApp.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="Frist Name Is Required")]
        public string FName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]
		public string LName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Required to Agree")]
		public bool IsAgree { get; set; }
	}
}

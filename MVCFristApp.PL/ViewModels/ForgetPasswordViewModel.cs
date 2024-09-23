using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCFristApp.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Email Address not valid")]
        public string Email { get; set; }
    }
}

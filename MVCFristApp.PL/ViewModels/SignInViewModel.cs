﻿using System.ComponentModel.DataAnnotations;

namespace MVCFristApp.PL.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="Email Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

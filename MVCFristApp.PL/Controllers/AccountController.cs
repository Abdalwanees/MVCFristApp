using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.Healpers;
using MVCFristApp.PL.Helpers; // Updated namespace spelling
using MVCFristApp.PL.ViewModels;
using System.Threading.Tasks;

namespace MVCFristApp.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailSettings _emailSettings;  // Inject EmailSettings

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 EmailSettings emailSettings)  // Add EmailSettings to constructor
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSettings = emailSettings;  // Assign injected EmailSettings
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.FName + " " + model.LName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }

                        ModelState.AddModelError(string.Empty, "Login Failed");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                // دائماً إرسال رسالة "تحقق من بريدك" بغض النظر عن وجود المستخدم
                if (user != null)
                {
                    // توليد رمز إعادة تعيين كلمة المرور
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);
                    var email = new Email
                    {
                        Subject = "Reset Your Password",
                        Body = $"<p>Please reset your password by clicking the following link: <a href='{resetPasswordUrl}'>Reset Password</a></p>",
                        Reciepints = model.Email
                    };
                    _emailSettings.SendEmail(email);
                }

                return RedirectToAction(nameof(CheckYourInBox));
            }
            return View(model);
        }


        public IActionResult CheckYourInBox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {
                string email = TempData["Email"] as string;
                string token = TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (resetPasswordResult.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in resetPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}

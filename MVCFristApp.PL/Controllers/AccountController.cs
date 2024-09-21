using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.ViewModels;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MVCFristApp.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var User = new ApplicationUser
                {
                    UserName = model.FName + " " + model.LName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };

                var Result = await _userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }

                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
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
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User != null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(User, model.Password, true, true);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }

                        ModelState.AddModelError(string.Empty, errorMessage: "LogIn Faild");

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
    }
}

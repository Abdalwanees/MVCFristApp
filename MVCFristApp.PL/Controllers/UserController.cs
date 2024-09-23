using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCFristApp.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var User = await _userManager.Users.Select(U => new UserViewModel
                {
                    Id = U.Id,
                    Email = U.Email,
                    FName = U.FName,
                    LName = U.LName,
                    phoneNumbre = U.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(User);
            }
            else
            {
                var User = await _userManager.FindByEmailAsync(email);
                if (User == null)
                {
                    var MappedUser = new UserViewModel
                    {
                        Id = User.Id,
                        Email = User.Email,
                        FName = User.FName,
                        LName = User.LName,
                        phoneNumbre = User.PhoneNumber,
                        Roles =_userManager.GetRolesAsync(User).Result
                    };
                    return View(new List<UserViewModel> { MappedUser});
                }
            }
            return View(Enumerable.Empty<UserViewModel>());
        }
    }
}

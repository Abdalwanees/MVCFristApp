using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
using MVCFristApp.DAL.Data;
using MVCFristApp.DAL.Models;
using System.Collections;

namespace MVCFristApp.PL.Extensions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {

                    config.Password.RequiredUniqueChars = 2;
                    config.Password.RequireDigit = true;
                    config.Password.RequireLowercase = true;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireNonAlphanumeric = true;
                    config.User.RequireUniqueEmail = true;
                    config.Lockout.MaxFailedAccessAttempts = 5;
                    config.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
                    config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
                }).AddEntityFrameworkStores<AppDbContext>();

            //services.AddAuthentication("Cookies");// Default schema of Authentication
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie("Hmada", config =>
            //{
            //    config.LoginPath = "/Account/signIn";
            //    config.AccessDeniedPath = "/Home/Error";
            //});

            services.ConfigureApplicationCookie(
                config =>
                {
                    config.LoginPath = "/Account/SignIn"; 
                    config.ExpireTimeSpan = System.TimeSpan.FromDays(5);
                    config.SlidingExpiration = true;
                });

            //-->Inside This we can regiseter this three servicess

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();
            return services;
        }
    }
}

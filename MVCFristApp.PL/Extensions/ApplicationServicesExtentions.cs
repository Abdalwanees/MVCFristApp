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
                config => {

                    config.Password.RequiredUniqueChars = 2;
                    config.Password.RequireDigit=true;
                    config.Password.RequireLowercase=true;
                    config.Password.RequireUppercase=true;
                    config.Password.RequireNonAlphanumeric=true;
                    config.User.RequireUniqueEmail=true;
                    config.Lockout.MaxFailedAccessAttempts=5;
                    config.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
                    config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
                }).AddEntityFrameworkStores<AppDbContext>();
            //-->Inside This we can regiseter this three servicess

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();
            return services;
        }
    }
}

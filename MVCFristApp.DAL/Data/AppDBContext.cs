using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCFristApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.DAL.Data
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
            // Customize table names for Identity models
            modelBuilder.Entity<IdentityUser>().ToTable("Users"); // جدول المستخدمين
            modelBuilder.Entity<IdentityRole>().ToTable("Roles"); // جدول الأدوار
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("User_Roles"); // جدول الربط بين المستخدمين والأدوار
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("User_Claims"); // جدول مطالبات المستخدم
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("User_Logins"); // جدول تسجيل دخول المستخدم
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("Role_Claims"); // جدول مطالبات الأدوار
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("User_Tokens"); // جدول الرموز (Tokens) الخاصة بالمستخدمين
        }
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }
         
        //public IdentityUser Users { get; set; }
        //public IdentityRole Roles { get; set; }

    }
}

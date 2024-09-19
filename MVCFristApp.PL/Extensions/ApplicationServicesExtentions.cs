using Microsoft.Extensions.DependencyInjection;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
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
            return services;
        }
    }
}

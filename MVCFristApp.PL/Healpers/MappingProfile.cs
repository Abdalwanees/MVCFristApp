using AutoMapper;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.ViewModels;

namespace MVCFristApp.PL.Healpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }

    }
}

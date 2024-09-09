using Microsoft.AspNetCore.Mvc;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;

namespace MVCFristApp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _departmentRepository = repository;
        }
        public IActionResult Index()
        {
            //GetAll()
            var Department=_departmentRepository.GetAll();
            return View(Department);//View with same Action
        }
        
    }
}

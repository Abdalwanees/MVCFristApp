using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
using MVCFristApp.DAL.Models;
using System;

namespace MVCFristApp.PL.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _enviroment;
        public DepartmentController(IDepartmentRepository repository,IWebHostEnvironment enviroment)
        {
            _departmentRepository = repository;
            _enviroment = enviroment;
        }
        public IActionResult Index()
        {
            //GetAll()
            var Department = _departmentRepository.GetAll();
            return View(Department);//View with same Action
        }
        //Create New Department
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Department departmrnt)
        {
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Add(departmrnt);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departmrnt);
        }
        // Details
        [HttpGet]
        public IActionResult Details(int? Id ,string Viewname= "Details")
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            var department=_departmentRepository.GetById(Id.Value);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View(Viewname,department);
            }
        }
        //Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id,"Update"); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int Id,Department department)
        {
            if (Id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            else
            {
                _departmentRepository.Update(department);
                return RedirectToAction("Index");
            }
        }
        //Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int Id,Department department)
        {
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_enviroment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occurred During Delete Department");

                return View(department);
            }
        }

    }
}

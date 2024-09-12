using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
using MVCFristApp.DAL.Models;
using System;

namespace MVCFristApp.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _environment;
        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment environment)
        {
            _employeeRepository = repository;
            _environment = environment;
        }
        //Get All
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        //Details
        [HttpGet]
        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            var deptDetails = _employeeRepository.GetById(Id.Value);
            if (deptDetails == null)
            {
                return NotFound();
            }
            return View(ViewName, deptDetails);
        }

        // Add new employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee) {
            if (ModelState.IsValid) {

                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);

        }

        //Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int Id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int? id) {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int Id,Employee employee) {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occurred During Delete Employee");

                return View(employee);
            }
        }
    }
}

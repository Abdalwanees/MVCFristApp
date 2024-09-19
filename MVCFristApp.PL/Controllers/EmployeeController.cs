using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.Helpers;
using MVCFristApp.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace MVCFristApp.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _iMapper;

        public EmployeeController(/*IEmployeeRepository repository,IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork ,IWebHostEnvironment environment,IMapper iMapper)
        {
            //_employeeRepository = repository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _environment = environment;
            _iMapper = iMapper;
        }
        //Get All
        //[HttpGet]
        public IActionResult Index(string searchInpt)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(searchInpt))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(searchInpt.ToLower());
                ViewBag.SearchTerm = searchInpt;
            }
            var MappedEmployees=_iMapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployees);
        }

        //Details
        [HttpGet]
        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            var empDetails = _unitOfWork.EmployeeRepository.GetById(Id.Value);
            var mappedEmployee = _iMapper.Map<Employee, EmployeeViewModel>(empDetails);
            if (mappedEmployee == null)
            {
                return NotFound();
            }
            return View(ViewName, mappedEmployee);
        }

        // Add new employee
        [HttpGet]
        public IActionResult Create()
        {
            var departmentNames= _unitOfWork.DepartmentRepository.GetAll();
            ViewData["Departments"]=departmentNames;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM) {
            if (ModelState.IsValid) {
                // Manual Mapping
                employeeVM.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "Images");

                var mappedEmployee = new Employee()
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name,
                    Salary = employeeVM.Salary,
                    Age = employeeVM.Age,
                    IsDeleted = employeeVM.IsDeleted,
                    IsActive = employeeVM.IsActive,
                    HireDate = employeeVM.HireDate,
                    Email = employeeVM.Email,
                    Phone = employeeVM.Phone,
                    workForId = employeeVM.workForId,
                    ImageUrl = employeeVM.ImageUrl,
                    Gender = (DAL.Models.Gender)employeeVM.Gender,
                };
                _unitOfWork.EmployeeRepository.Add(mappedEmployee);
                var count = _unitOfWork.Compelete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employeeVM);

        }

        //Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            var departmentNames = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartment= _iMapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(departmentNames);
            ViewData["Departments"] = departmentNames;
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int Id, EmployeeViewModel employeeVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                employeeVm.ImageUrl = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                var MappedEmployee = _iMapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                _unitOfWork.Compelete();
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
        public IActionResult Delete([FromRoute]int Id,EmployeeViewModel employeevm) {
            try
            {
                var mappedEmployee = _iMapper.Map<EmployeeViewModel,Employee>(employeevm);  
                _unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                _unitOfWork.Compelete();
                DocumentSettings.DeleteFile(employeevm.ImageUrl,"Images");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occurred During Delete Employee");

                return View(employeevm);
            }
        }
    }
}

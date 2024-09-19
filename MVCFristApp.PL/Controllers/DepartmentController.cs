using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCFristApp.BLL.Interfaces;
using MVCFristApp.BLL.Repositories;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVCFristApp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _enviroment;
        private readonly IMapper _imapper;

        public DepartmentController(/*IDepartmentRepository repository*/IUnitOfWork unitOfWork,IWebHostEnvironment enviroment,IMapper imapper)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = repository;
            _enviroment = enviroment;
            _imapper = imapper;
        }
        public IActionResult Index()
        {
            //GetAll()
            var department = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartment = _imapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(department);
            return View(mappedDepartment);//View with same Action
        }
        //Create New Department
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment=_imapper.Map<DepartmentViewModel,Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDepartment);
                var Count = _unitOfWork.Compelete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departmentVM);
        }
        // Details
        [HttpGet]
        public IActionResult Details(int? Id ,string Viewname= "Details")
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            var department=_unitOfWork.DepartmentRepository.GetById(Id.Value);
            var mappedDepartment = _imapper.Map<Department, DepartmentViewModel>(department);
            if (mappedDepartment == null)
            {
                return NotFound();
            }
            else
            {
                return View(Viewname, mappedDepartment);
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
        public IActionResult Update([FromRoute]int Id,DepartmentViewModel departmentVM)
        {
            if (Id != departmentVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            else
            {
                var mappedDepartment = _imapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Update(mappedDepartment);
                _unitOfWork.Compelete();
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
        public IActionResult Delete([FromRoute]int Id,DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDepartment = _imapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Delete(mappedDepartment);
                _unitOfWork.Compelete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_enviroment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occurred During Delete Department");

                return View(departmentVM);
            }
        }

    }
}

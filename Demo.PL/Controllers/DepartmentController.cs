using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartment = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartment);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = _mapper.Map<Department>(department);
               await _unitOfWork.DepartmentRepository.Add(mappedDepartment);
               // TempData["Message"] = "Hello from (Create=>Index)[TempData]";
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _unitOfWork.DepartmentRepository.GetById(id);

            var mappedDepartment =_mapper.Map<DepartmentViewModel>(department);
            if (department == null) return NotFound();

            return View(mappedDepartment);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var department = await _unitOfWork.DepartmentRepository.GetById(id);
            var mappedDepartment = _mapper.Map<DepartmentViewModel>(department);
            if (department == null) return NotFound();

            return View(mappedDepartment);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, DepartmentViewModel department)
        {
            if (id != department.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = _mapper.Map<Department>(department);

                    await _unitOfWork.DepartmentRepository.Update(mappedDepartment);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                    return View(department);
                }

            }

            return View(department);

        }

     
        public async Task<IActionResult> Delete(int? id , Department department)
        {
            if (id != department.Id) return NotFound();
            try
            {
                await _unitOfWork.DepartmentRepository.Delete(department);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

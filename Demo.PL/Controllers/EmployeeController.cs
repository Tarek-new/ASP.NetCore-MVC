using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(/*IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository*/
           IUnitOfWork unitOfWork , IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper =mapper;
        }
        public async Task<IActionResult> Index( string SearchValue="")
        {
            IEnumerable<Employee> employees;
            if (String.IsNullOrEmpty(SearchValue))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAll();
             
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.Search(SearchValue);
            }
            //var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployees);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel )
        {
            if (ModelState.IsValid)
            {
                ////Manual Mapping
                //var MappedEmployee = new Employee
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Age = employee.Age,

                //};
                
               
                var MappedEmployee=_mapper.Map<Employee>(employeeViewModel);
                await _unitOfWork.EmployeeRepository.Add(MappedEmployee);
                return RedirectToAction("Index");
            }

            return View(employeeViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.GetById(id);
          

            var department = await _unitOfWork.EmployeeRepository.GetDepartmentByEmployeeId(id);
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            var mappedDepartment = _mapper.Map<DepartmentViewModel>(department);

            if (mappedEmployee == null) return NotFound();

            return View(mappedEmployee);
        }

        public async Task<IActionResult> update(int? id)
        {
            ViewBag.departments = await _unitOfWork.DepartmentRepository.GetAll();

            if (id == null) return NotFound();

            var employee = await _unitOfWork.EmployeeRepository.GetById(id);
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);


            if (employee == null) return NotFound();

            return View(mappedEmployee);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel employeeViewModel)
        {

            if (id != employeeViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _unitOfWork.EmployeeRepository.GetById(employeeViewModel.Id);

                    if (employeeViewModel.Image != null)
                    {
                        string oldFileName = existingEmployee.ImageURL != null ? 
                            Path.GetFileName(new Uri(existingEmployee.ImageURL).AbsolutePath) : null;
                        employeeViewModel.ImageURL = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs", oldFileName);
                    }
                    else
                    {
                        employeeViewModel.ImageURL = existingEmployee.ImageURL;
                    }

                    _mapper.Map(employeeViewModel, existingEmployee);

                    await _unitOfWork.EmployeeRepository.Update(existingEmployee);
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {

                    return View(employeeViewModel);
                }
            }

            return View(employeeViewModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
           
            var employee= await _unitOfWork.EmployeeRepository.GetById(id);

            if(employee is null) return NotFound();

            DocumentSettings.DeleteFile("Imgs", employee.ImageURL);
            await _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
                   
        }
    }
}


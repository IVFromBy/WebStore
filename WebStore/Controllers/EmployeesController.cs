using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entites.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("staff")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("all")]
        public IActionResult Index() => View(_EmployeesData.Get());

        public IActionResult Details(int Id)
        {
            var _employee = _EmployeesData.Get(Id);

            if (_employee is not null)
                return View(_employee);

            return RedirectToAction("NotFound", "Home");

        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit
        //[Route("info(id-{id})")]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new EmployeeViewModel());

            if (Id <= 0) return BadRequest();

            var employee = _EmployeesData.Get((int)Id);

            if (employee is null)
                return RedirectToAction("NotFound", "Home");

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Age = employee.Age,
                DateOfHiring = employee.DateOfHiring,
                Education = employee.Education,
                Experience = employee.Experience,
                FirstName = employee.FirstName,
                IQ = employee.IQ,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                PhoneNumber = employee.PhoneNumber,
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeViewModel pEmp)
        {
            if (pEmp is null)
                throw new ArgumentNullException(nameof(pEmp));

            if (pEmp.FirstName == "Admin")
                ModelState.AddModelError("", "Админы уже есть");

            if (!ModelState.IsValid)
            {
                return View(pEmp);
            }
            var employee = new Employee
            {
                Id = pEmp.Id,
                Age = pEmp.Age,
                DateOfHiring = pEmp.DateOfHiring,
                Education = pEmp.Education,
                Experience = pEmp.Experience,
                FirstName = pEmp.FirstName,
                IQ = pEmp.IQ,
                LastName = pEmp.LastName,
                Patronymic = pEmp.Patronymic,
                PhoneNumber = pEmp.PhoneNumber,
            };

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(Id);

            if (employee is null)
                return RedirectToAction("NotFound", "Home");

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Age = employee.Age,
                DateOfHiring = employee.DateOfHiring,
                Education = employee.Education,
                Experience = employee.Experience,
                FirstName = employee.FirstName,
                IQ = employee.IQ,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                PhoneNumber = employee.PhoneNumber,
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteAction(int Id)
        {
            _EmployeesData.Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}

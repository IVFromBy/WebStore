using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private List<Employee> _Employees;
        public EmployeesController()
        {
            _Employees = TestData.Employees;

        }
        public IActionResult Index() => View(_Employees);

        public IActionResult Details(int Id)
        {
            var _employee = _Employees.FirstOrDefault(x => x.Id == Id);

            if (_employee is not null)
                return View(_employee);

            return NotFound();

        }

        public IActionResult Edit(int Id)
        {
            var _employee = _Employees.FirstOrDefault(x => x.Id == Id);

            if (_employee is not null)
                return View(_employee);

            return NotFound();

        }

        public IActionResult EditAction(Employee pEmp)
        {
            int? index = _Employees.FindIndex(x => x.Id == pEmp.Id);

            if (index != null)
            {
                _Employees[index.Value] = pEmp;
                return View("Index",_Employees);
            }
            return View("../Home/NotFound");

        }

        public IActionResult Delete(int Id)
        {
            var _employee = _Employees.FirstOrDefault(x => x.Id == Id);

            if (_employee is not null)
                return View(_employee);

            return NotFound();
        }
        
        
        public IActionResult DeleteAction(int? Id)
        {
            if (Id != null)
            {
                var _employee = _Employees.FirstOrDefault(x => x.Id == Id);
                if (_employee != null)
                {
                    _Employees.Remove(_employee);

                    return View("Index",_Employees);
                }
            }
            return View("../Home/NotFound");
        }
    }
}

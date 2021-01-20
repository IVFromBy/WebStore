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
    }
}

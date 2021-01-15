using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "AAA", FirstName = "aaa", Patronymic = "-a", Age = 21 },
            new Employee { Id = 1, LastName = "BBB", FirstName = "bbb", Patronymic = "-b", Age = 22 },
            new Employee { Id = 1, LastName = "CCC", FirstName = "ccc", Patronymic = "-c", Age = 31 },
        };

        public IActionResult Index() => View("Sec");

        public IActionResult Second()
        {
            return Content("Sec Controller Aaction");
        }

        public IActionResult Employees() => View(__Employees);
    }
}

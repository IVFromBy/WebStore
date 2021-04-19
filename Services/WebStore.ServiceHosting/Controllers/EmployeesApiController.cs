using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Emploees)]
    [ApiController]
    public class EmployeesApiController : Controller, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;
        private readonly ILogger<IEmployeesData> _logger;

        public EmployeesApiController(IEmployeesData employeesData, ILogger<IEmployeesData> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        [HttpPost]
        public int Add(Employee employee)
        {
            _logger.LogInformation("Добавление сотрудника {0}", employee);
            
            return _employeesData.Add(employee);
        }

        //[Httppost("employee")] ////http://locvalhost:5001/api/employees/employee?LastName=Black&FirstName=Bell&patranomic=CG
        //public Employee Add(string LastName, string FirstName, string Patronymic) => _employeesData.Add(LastName, FirstName, Patronymic);

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation("Удаление сотрудника id:{0}..", id);
            
            var result =_employeesData.Delete(id);

            _logger.LogInformation("Удаление сотрудника id:{0}  - {1}", id, result ? "выполнение" : "не найден");
            
            return result;
        }

        [HttpGet] //http://locvalhost:5001/api/employees
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")] //http://locvalhost:5001/api/employees/5
        public Employee Get(int id) => _employeesData.Get(id);

        //[HttpGet("employee")] ////http://locvalhost:5001/api/employees/employee?LastName=Black&FirstName=Bell&patranomic=CG
        //public Employee GetByname(string LastName,string FirstName, string Patronymic) => _employeesData.GetByName(LastName,FirstName,Patronymic)

        [HttpPut]
        public void Update(Employee employee)
        {
            _logger.LogInformation("Редактирование сотрудника {0}", employee);
            _employeesData.Update(employee);
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : Controller, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => _employeesData = employeesData;

        [HttpPost]
        public int Add(Employee employee) => _employeesData.Add(employee);
        
        //[Httppost("employee")] ////http://locvalhost:5001/api/employees/employee?LastName=Black&FirstName=Bell&patranomic=CG
        //public Employee Add(string LastName, string FirstName, string Patronymic) => _employeesData.Add(LastName, FirstName, Patronymic);
        
        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);
        
        [HttpGet] //http://locvalhost:5001/api/employees
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")] //http://locvalhost:5001/api/employees/5
        public Employee Get(int id) => _employeesData.Get(id);

        //[HttpGet("employee")] ////http://locvalhost:5001/api/employees/employee?LastName=Black&FirstName=Bell&patranomic=CG
        //public Employee GetByname(string LastName,string FirstName, string Patronymic) => _employeesData.GetByName(LastName,FirstName,Patronymic)

        [HttpPut]
        public void Update(Employee employee) => _employeesData.Update(employee);
    }
}

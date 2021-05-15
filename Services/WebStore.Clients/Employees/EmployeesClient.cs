using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger) : base(configuration, WebAPI.Emploees) => _logger = logger;

        public int Add(Employee employee) => Post(Addres, employee).Content.ReadAsAsync<int>().Result;

        //public Employee Add(string LastName, string FirstName, string Patronymic) =>
        //Post<Employee>($"{Addres}/employees?LastName={LastName}&FirtsName={FirstName}&Patronymic={Patronymic}", "")
        //.Content.ReadAsAsync<Employee>().Result;

        public bool Delete(int id)
        {
            _logger.LogInformation("Удаление сотрудника id:{0} ...", id);
            using (_logger.BeginScope("Удаление сотрудника id:{0} ...", id))
            {
                var result = Delete($"{Addres}/{id}").IsSuccessStatusCode;

                _logger.LogInformation("Удаление сотрудника id:{0}  - {1}", id, result ? "выполнение" : "не найден");

                return result;
            }
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Addres);

        public Employee Get(int id) => Get<Employee>($"{Addres}/{id}");

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            Get<Employee>($"{Addres}/employees?LastName={LastName}&FirtsName={FirstName}&Patronymic={Patronymic}");


        public void Update(Employee employee) => Put(Addres, employee);


    }
}

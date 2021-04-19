using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration ) : base(configuration, WebAPI.Emploees) { }

        public int Add(Employee employee) => Post(Addres, employee).Content.ReadAsAsync<int>().Result;

        //public Employee Add(string LastName, string FirstName, string Patronymic) =>
        //Post<Employee>($"{Addres}/employees?LastName={LastName}&FirtsName={FirstName}&Patronymic={Patronymic}", "")
        //.Content.ReadAsAsync<Employee>().Result;

        public bool Delete(int id) => Delete($"{Addres}/{id}").IsSuccessStatusCode;

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Addres);

        public Employee Get(int id) => Get<Employee>($"{Addres}/{id}");

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            Get<Employee>($"{Addres}/employees?LastName={LastName}&FirtsName={FirstName}&Patronymic={Patronymic}");


        public void Update(Employee employee) => Put(Addres, employee);

        
    }
}

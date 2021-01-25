using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee Gey(int id);

        int Add(Employee employee);

        void Update(Employee employee);

        bool Delete(int id);

    }
}

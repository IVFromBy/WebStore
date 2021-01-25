using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Patronymic { get; init; }
        public int Age { get; init; }
        public DateTime DateOfHiring { get; init; }
        public int Experience { get; init; }
        public string Education { get; init; }
        public int IQ { get; set; }
        public string PhoneNumber { get; init; }
    }
}

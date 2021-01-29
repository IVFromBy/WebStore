using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; init; }
        [Required(AllowEmptyStrings =false,
                  ErrorMessage = "Необходимо заполнить имя")]
        public string FirstName { get; init; }
        [Required(AllowEmptyStrings = false,
                  ErrorMessage = "Необходимо заполнить фамилию")]
        public string LastName { get; init; }
        public string Patronymic { get; init; }
        [Required(AllowEmptyStrings = false,
          ErrorMessage = "Необходимо указать возраст")]
        public int Age { get; init; }
        public DateTime DateOfHiring { get; init; }
        public int Experience { get; init; }
        public string Education { get; init; }
        public int IQ { get; set; }
        public string PhoneNumber { get; init; }
    }
}

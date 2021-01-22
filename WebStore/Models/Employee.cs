using System;

namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public DateTime DateOfHiring { get; set; }
        public int Experience { get; set; }
        public string Education { get; set; }
        public int IQ { get; set; }
        public string PhoneNumber { get; set; }

        public Employee()
        {
            Id = 0;
            FirstName = "Имя"; 
            LastName = "Фамилия";
            Patronymic = "Отчество";
            Age = 18;
            DateOfHiring = DateTime.Today;
            Experience = 0;
            Education = "Среднее";
            IQ = 110;
            PhoneNumber = "0-00-00";
                
        }
    }
}

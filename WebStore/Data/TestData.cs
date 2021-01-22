using System;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee
            {
                Id = 1,
                LastName = "Aстанов",
                FirstName = "Артур",
                Patronymic = "Александрович",
                Age = 21
                      ,
                DateOfHiring = DateTime.Parse("2020-12-12"),
                Education = "Среднее"
                      ,
                Experience = 0,
                IQ = 110,
                PhoneNumber = "4-35-22"
            },
            new Employee
            {
                Id = 2,
                LastName = "Бегунок",
                FirstName = "Баграт",
                Patronymic = "Борисович"
             ,
                Age = 22,
                DateOfHiring = DateTime.Parse("2019-01-30"),
                Education = "Среднее",
                Experience = 1,
                IQ = 130,
                PhoneNumber = "4-12-34"
            },
            new Employee
            {
                Id = 3,
                LastName = "Васерман",
                FirstName = "Валентин",
                Patronymic = "Владимирович"
             ,
                Age = 31,
                DateOfHiring = DateTime.Parse("2018-01-30"),
                Education = "Высшее",
                Experience = 6,
                IQ = 140,
                PhoneNumber = "4-15-10"
            },
        };

    }
}

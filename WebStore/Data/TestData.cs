using System;
using System.Collections.Generic;
using WebStore.Domain.Entites;
using WebStore.Models;
using WebStore.Infrastructure.Services.InSql;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Aстанов", FirstName = "Артур", Patronymic = "Александрович", Age = 21, DateOfHiring = DateTime.Parse("2020-12-12"), Education = "Среднее", Experience = 0, IQ = 110, PhoneNumber = "4-35-22" },
            new Employee { Id = 2, LastName = "Aстанов", FirstName = "Артур", Patronymic = "Александрович", Age = 22, DateOfHiring = DateTime.Parse("2019-12-12"), Education = "Среднее", Experience = 1, IQ = 120, PhoneNumber = "4-10-10" },
            new Employee { Id = 3, LastName = "Aстанов", FirstName = "Артур", Patronymic = "Александрович", Age = 32, DateOfHiring = DateTime.Parse("2015-12-12"), Education = "Высшее", Experience = 5, IQ = 140, PhoneNumber = "7-88-02" }
        };

        public static IEnumerable<Section> Sections { get; } = new[]
       {
              new Section { Name = "Спорт", Order = 0 },
              new Section { Name = "Nike", Order = 0, ParentId = 1 },
              new Section { Name = "Under Armour", Order = 1, ParentId = 1 },
              new Section { Name = "Adidas", Order = 2, ParentId = 1 },
              new Section { Name = "Puma", Order = 3, ParentId = 1 },
              new Section { Name = "ASICS", Order = 4, ParentId = 1 },
              new Section { Name = "Для мужчин", Order = 1 },
              new Section { Name = "Fendi", Order = 0, ParentId = 7 },
              new Section { Name = "Guess", Order = 1, ParentId = 7 },
              new Section { Name = "Valentino", Order = 2, ParentId = 7 },
              new Section { Name = "Диор", Order = 3, ParentId = 7 },
              new Section { Name = "Версачи", Order = 4, ParentId = 7 },
              new Section { Name = "Армани", Order = 5, ParentId = 7 },
              new Section { Name = "Prada", Order = 6, ParentId = 7 },
              new Section { Name = "Дольче и Габбана", Order = 7, ParentId = 7 },
              new Section { Name = "Шанель", Order = 8, ParentId = 7 },
              new Section { Name = "Гуччи", Order = 9, ParentId = 7 },
              new Section { Name = "Для женщин", Order = 2 },
              new Section { Name = "Fendi", Order = 0, ParentId = 18 },
              new Section { Name = "Guess", Order = 1, ParentId = 18 },
              new Section { Name = "Valentino", Order = 2, ParentId = 18 },
              new Section { Name = "Dior", Order = 3, ParentId = 18 },
              new Section { Name = "Versace", Order = 4, ParentId = 18 },
              new Section { Name = "Для детей", Order = 3 },
              new Section { Name = "Мода", Order = 4 },
              new Section { Name = "Для дома", Order = 5 },
              new Section { Name = "Интерьер", Order = 6 },
              new Section { Name = "Одежда", Order = 7 },
              new Section { Name = "Сумки", Order = 8 },
              new Section { Name = "Обувь", Order = 9 },
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Name = "Acne", Order = 0 },
            new Brand { Name = "Grune Erde", Order = 1 },
            new Brand { Name = "Albiro", Order = 2 },
            new Brand { Name = "Ronhill", Order = 3 },
            new Brand { Name = "Oddmolly", Order = 4 },
            new Brand { Name = "Boudestijn", Order = 5 },
            new Brand { Name = "Rosch creative culture", Order = 6 },
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
            new Product { Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
            new Product { Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
            new Product { Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
            new Product { Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
            new Product { Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
            new Product { Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
            new Product { Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
            new Product { Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
            new Product { Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
            new Product { Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
            new Product { Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
        }; 

    }
}

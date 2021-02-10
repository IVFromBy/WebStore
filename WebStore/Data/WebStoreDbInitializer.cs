using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _Logger;

        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public void Initiallize()
        {
            _Logger.LogInformation("Инициализация базы данных...");
            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            var db = _db.Database;
            if (db.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("Выполенние миграции...");
                db.Migrate();
                _Logger.LogInformation("Выполенние миграции выполенно успешно");
            }
            else
            {
                _Logger.LogInformation("База данных находится в последней версии");
            }

            try
            {
                InitializeProducts();
            }
            catch (Exception error)
            {

                _Logger.LogError(error, "Ошибка при выполении инициализации БД");
                throw;
            }
            _Logger.LogInformation("Инициализация БД - успех");
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _Logger.LogInformation("Инициализация БД товарами не требуется...");
                return;
            }

            _Logger.LogInformation("Инициализация товаров...");
            _Logger.LogInformation("Добавление секций...");

            //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");

            var products_section = TestData.Sections.Join(
                TestData.Products,
                s => s.Id,
                p => p.SectionId,
                (section, product) => (section, product));

            foreach (var (section, product) in products_section)
            {
                section.Products.Add(product);
            }

            var products_brand = TestData.Brands.Join(
                TestData.Products,
                b => b.Id,
                p => p.BrandId,
                (brand, product) => (brand, product));

            foreach (var (brand, product) in products_brand)
            {
                brand.Products.Add(product);
            }

            var section_section = TestData.Sections.Join(
                TestData.Sections,
                parent_section => parent_section.Id,
                child_section => child_section.ParentId,
                (parent,child) => (parent, child));

            foreach(var (parent,child) in section_section)
            {
                child.Parent = parent;
            }    

            foreach(var product in TestData.Products)
            {
                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = 0;
            }
            
            foreach(var section in TestData.Sections)
            {
                section.Id = 0;
                section.Parent = null;
            }

            foreach (var brand in TestData.Brands)
            {
                brand.Id = 0;                
            }

            using (_db.Database.BeginTransaction())
            {

                _db.Products.AddRange(TestData.Products);
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);

                //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");

                _db.SaveChanges();


                _db.Database.CommitTransaction();
            }
           

            _Logger.LogInformation("Инициализация товаров выполнена успешно");
        }
    }
}

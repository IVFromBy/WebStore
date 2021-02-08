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

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);

                //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");

                _db.SaveChanges();

                //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Добавление секций - успех");

            _Logger.LogInformation("Добавление брендов...");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Добавление брендов - успех");

            _Logger.LogInformation("Добавление товаров...");
            using (_db.Database.BeginTransaction())
            {

                var products = TestData.Products;
                var rnd = new Random();
                var section_min = _db.Sections.Min(s => s.Id);
                var section_max = _db.Sections.Max(s => s.Id);
                var brand_min = _db.Brands.Min(s => s.Id);
                var brand_max = _db.Brands.Max(s => s.Id);

                foreach (var product in products)
                {

                    product.SectionId = rnd.Next(section_min, section_max);
                    product.BrandId = rnd.Next(brand_min, brand_max);
                }

                _db.Products.AddRange(products);
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            _Logger.LogInformation("Добавление товаров - успех");

            _Logger.LogInformation("Инициализация товаров выполнена успешно");
        }
    }
}

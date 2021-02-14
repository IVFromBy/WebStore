using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entites.Identity;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _Logger;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public WebStoreDbInitializer(
            WebStoreDB db,
            ILogger<WebStoreDbInitializer> Logger,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager
            )
        {
            _db = db;
            _Logger = Logger;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
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
                InitializeIdetityAsync().Wait();
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

        private async Task InitializeIdetityAsync()
        {
            _Logger.LogInformation("Инициализация системы Identity...");

            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Роль {0} отсутствует. Создаю...", RoleName);

                    await _RoleManager.CreateAsync(new Role { Name = RoleName });

                    _Logger.LogInformation("Роль {0} создана успешно",RoleName);
                }
            }

            await CheckRole(Role.Administrator);
            await CheckRole(Role.User);
            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Отсутствует учётная запись администратора");
                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("Учётная запись администратора создана успешно");
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                    _Logger.LogInformation("Учётная запись администратора наделена ролью {0}", Role.Administrator);
                }    
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании учётной записи администратора: {string.Join(",", errors)}");
                }
            }

            _Logger.LogInformation("Инициализация системы Identity завершена успешно");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entites.DTO;
using WebStore.Domain.Entites.Identity;
using WebStore.Domain.Entites.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.Services.Mapping;

namespace WebStore.Infrastructure.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<SqlOrderService> _logger;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager, ILogger<SqlOrderService> logger)
        {
            _db = db;
            _UserManager = userManager;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);

            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден в БД");
            _logger.LogInformation("Оформление нового заказа для {UserName}");
            var timer = Stopwatch.StartNew();
            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user,


            };

            //var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            //var cart_product = await _db.Products
            //    .Where(p => product_ids.Contains(p.Id))
            //    .ToArrayAsync();

            //order.Items = Cart.Items.Join(

            //    cart_product,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,
            //        Quantty = cart_item.Quantity,
            //    }).ToArray();
            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null) continue;
                var order_item = new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price,
                    Quantty = item.Quantity,
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Заказ для {0} успешно сформирован за {1}с id:{2}", UserName, timer.Elapsed, order.Id);
            return order.ToDto();
        }

        public async Task<OrderDto> GetOrderById(int id) => (await _db.Orders
                 .Include(order => order.User)
                 .Include(order => order.Items)
                 .FirstOrDefaultAsync(order => order.Id == id)).ToDto();

        public async Task<IEnumerable<OrderDto>> GetUserOrders(string UserName) => (await _db.Orders
                 .Include(order => order.User)
                 .Include(order => order.Items)
                 .Where(order => order.User.UserName == UserName)
                 .ToArrayAsync())
            .Select(order => order.ToDto());

    }
}

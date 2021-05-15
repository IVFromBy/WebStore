using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{/// <summary>
/// Управление заказами
/// </summary>
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService) => _orderService = orderService;
        /// <summary>
        /// Создание ноовго заказа
        /// </summary>
        /// <param name="UserName">Имя пользователя-заказчика</param>
        /// <param name="OrderModel">Информация о заказе</param>
        /// <returns>Информация о сформированном заказе</returns>
        [HttpPost("{UserName}")]
        public Task<OrderDto> CreateOrder(string UserName,[FromBody] CreateOrderModel OrderModel) => _orderService.CreateOrder(UserName, OrderModel);
        /// <summary>
        /// Получение заказа по его диентификатору
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <returns>Информация о заказе</returns>
        [HttpGet("{id}")]
        public Task<OrderDto> GetOrderById(int id) => _orderService.GetOrderById(id);
        /// <summary>
        /// Получение всех заказов пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Перечень заказов пользователя</returns>
        [HttpGet("user/{UserName}")]
        public Task<IEnumerable<OrderDto>> GetUserOrders(string UserName) => _orderService.GetUserOrders(UserName);
    }
}

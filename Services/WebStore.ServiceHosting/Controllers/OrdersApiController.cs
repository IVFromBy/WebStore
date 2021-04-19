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
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        [HttpPost("{UserName}")]
        public Task<OrderDto> CreateOrder(string UserName,[FromBody] CreateOrderModel OrderModel) => _orderService.CreateOrder(UserName, OrderModel);
        [HttpGet("{id}")]
        public Task<OrderDto> GetOrderById(int id) => _orderService.GetOrderById(id);
        [HttpGet("user/{UserName}")]
        public Task<IEnumerable<OrderDto>> GetUserOrders(string UserName) => _orderService.GetUserOrders(UserName);
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebAPI.Orders) { }

        public async Task<OrderDto> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var response = await PostAsync($"{Addres}/{UserName}", OrderModel);
            return await response.Content.ReadAsAsync<OrderDto>();

        }

        public async Task<OrderDto> GetOrderById(int id) => await GetAsync<OrderDto>($"{Addres}/{id}");


        public async Task<IEnumerable<OrderDto>> GetUserOrders(string UserName) => await GetAsync<IEnumerable<OrderDto>>($"{Addres}/user/{UserName}");
    }
}

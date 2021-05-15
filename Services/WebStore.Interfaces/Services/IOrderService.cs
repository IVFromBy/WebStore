using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entites.DTO;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrders(string UserName);

        Task<OrderDto> GetOrderById(int id);

        Task<OrderDto> CreateOrder(string UserName, CreateOrderModel OrderModel);

    }
}

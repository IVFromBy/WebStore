using System.Linq;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.Domain.Entites.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDto ToDto(this OrderItem Item) => Item is null ?
            null
            : new OrderItemDto
            {
                Id = Item.Id,
                Price = Item.Price,
                Quantity = Item.Quantty,
                ProductId = Item.Product?.Id ?? 0,

            };
        public static OrderItem FromDto(this OrderItemDto Item) => Item is null ?
            null
            : new OrderItem
            {
                Id = Item.Id,
                Price = Item.Price,
                Quantty = Item.Quantity,
                Product = new Product{Id = Item.ProductId},
            };

        public static OrderDto ToDto(this Order order) => order is null
            ? null
            : new OrderDto
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date,
                Items = order.Items.Select(ToDto),
            };
        public static Order FromDto(this OrderDto order) => order is null
            ? null
            : new Order
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date,
                Items = order.Items.Select(FromDto).ToList(),
            };
    }
}

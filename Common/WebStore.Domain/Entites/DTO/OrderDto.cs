using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Domain.Entites.DTO
{
    /// <summary>
    /// Информация о заказе
    /// </summary>
    public class OrderDto
    {/// <summary>
    /// id заказа
    /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя заказчика
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// дата заказа
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// перечень заказа
        /// </summary>
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
    /// <summary>
    /// пункт заказа
    /// </summary>
    public class OrderItemDto
    {/// <summary>
    /// id пункта
    /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// цена 
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// количество
        /// </summary>
        public int Quantity { get; set; }
    }
    /// <summary>
    /// Модель процесса создания заказа
    /// </summary>
    public class CreateOrderModel
    {/// <summary>
    /// Модель заказа
    /// </summary>
        public OrderViewModel  Order { get; set; }
        /// <summary>
        /// Перечень элементов заказа
        /// </summary>
        public IList<OrderItemDto> Items { get; set; }
    }

}

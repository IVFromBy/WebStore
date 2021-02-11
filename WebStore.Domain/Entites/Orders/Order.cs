using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entites.Base;
using WebStore.Domain.Entites.Identity;

namespace WebStore.Domain.Entites.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public User User { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    }

    public class OrderItem : Entity
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantty { get; set; }

        [NotMapped]
        public decimal TotalItemPrice => Price * Quantty;
    }
}

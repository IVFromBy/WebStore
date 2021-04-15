using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entites.Base;
using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderEntity
    {
        //[Column("BrandOrder")]
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public bool IsDeleted { get; set; } = false;
    }
}

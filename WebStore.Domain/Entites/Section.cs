using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entites.Base;
using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites
{
    public class Section : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section Parent { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public bool IsDeleted { get; set; } = false;
    }
}

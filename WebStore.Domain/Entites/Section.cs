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
        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

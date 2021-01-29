using WebStore.Domain.Entites.Base;
using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites
{
    public class Brand : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
    }
}

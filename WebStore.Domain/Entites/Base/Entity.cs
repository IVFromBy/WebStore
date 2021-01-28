using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

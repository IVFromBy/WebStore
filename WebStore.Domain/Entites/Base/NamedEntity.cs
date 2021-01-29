using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites.Base
{
    public abstract class NamedEntity : Entity, INameEntity
    {
        public string Name { get; set; }
    }
}

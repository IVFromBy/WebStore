using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites.Base
{
    public abstract class Entity : IEntity
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption)]
        public int Id { get; set; }
    }
}

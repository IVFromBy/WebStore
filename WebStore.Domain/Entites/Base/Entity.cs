using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entites.Base.Interfaces;

namespace WebStore.Domain.Entites.Base
{
    public abstract class Entity : IEntity
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] // если PK в базе имеет отличное название от id 
        public int Id { get; set; }
    }
}

#region

using System.ComponentModel.DataAnnotations;
using comrade.Domain.Interfaces;

#endregion

namespace comrade.Domain.Bases
{
    public abstract class Entity : IEntity
    {
        [Key] public int Id { get; set; }

        public virtual int Key => Id;

        public virtual string Value => ToString();
    }
}
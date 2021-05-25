#region

using comrade.Domain.Interfaces;

#endregion

namespace comrade.Core.Helpers.Interfaces
{
    public interface ISingleResult<TEntity> : IResult
        where TEntity : IEntity
    {
        TEntity Data { get; set; }
    }
}
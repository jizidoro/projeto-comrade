#region

using System.Threading.Tasks;
using comrade.Domain.Interfaces;

#endregion

namespace comrade.Core.Helpers.Interfaces
{
    public interface IEntityValidation<TEntity>
        where TEntity : IEntity
    {
        Task<ISingleResult<TEntity>> RegistroExiste(int id, params string[] includes);

        Task<ISingleResult<TEntity>> RegistroComMesmoCodigo(int id, string codigo);
    }
}
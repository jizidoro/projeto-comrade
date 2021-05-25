#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Bases;

#endregion

namespace comrade.Core.Helpers.Models.Validations
{
    public class EntityValidation<TEntity> : IEntityValidation<TEntity>
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

        public EntityValidation(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<TEntity>> RegistroExiste(int id, params string[] includes)
        {
            var entity = await _repository.GetById(id, includes);
            if (entity == null) return new SingleResult<TEntity>(MensagensNegocio.MSG04);

            return new SingleResult<TEntity>(entity);
        }

        public async Task<ISingleResult<TEntity>> RegistroComMesmoCodigo(int id, string codigo)
        {
            var result = await _repository.ValueExists(id, codigo);
            if (result) return new SingleResult<TEntity>(MensagensNegocio.MSG08);

            return new SingleResult<TEntity>();
        }
    }
}
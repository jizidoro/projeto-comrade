#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Interfaces;

#endregion

namespace comrade.Core.Helpers.Bases
{
    public class Service : IService
    {
        private readonly IUnitOfWork _uow;

        public Service(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Commit()
        {
            if (await _uow.Commit()) return true;

            return false;
        }

        public ISingleResult<T> ValidarEntidade<T>(T entity) where T : IEntity
        {
            var context = new ValidationContext(entity, null, null);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!valid)
            {
                var listaErros = validationResults.Select(x => x.ErrorMessage);
                return new SingleResult<T>(listaErros);
            }

            return new SingleResult<T>();
        }
    }
}
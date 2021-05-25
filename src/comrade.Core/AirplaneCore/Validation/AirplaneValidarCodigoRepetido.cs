#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validation
{
    public class AirplaneValidarCodigoRepetido : EntityValidation<Airplane>
    {
        private readonly IAirplaneRepository _repository;

        public AirplaneValidarCodigoRepetido(IAirplaneRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var result = await _repository.RegistroCodigoRepetido(entity.Id, entity.Codigo);

            return result;
        }
    }
}
#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validations
{
    public class AirplaneValidarEditar : EntityValidation<Airplane>
    {
        private readonly AirplaneValidarCodigoRepetido _airplaneValidarCodigoRepetido;
        private readonly IAirplaneRepository _repository;

        public AirplaneValidarEditar(IAirplaneRepository repository,
            AirplaneValidarCodigoRepetido airplaneValidarCodigoRepetido)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidarCodigoRepetido = airplaneValidarCodigoRepetido;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var registroExiste = await RegistroExiste(entity.Id);
            if (!registroExiste.Sucesso) return registroExiste;

            var registroCodigoRepetido = await _airplaneValidarCodigoRepetido.Execute(entity);
            if (!registroCodigoRepetido.Sucesso) return registroCodigoRepetido;

            registroCodigoRepetido.Data = registroExiste.Data;

            return registroCodigoRepetido;
        }
    }
}
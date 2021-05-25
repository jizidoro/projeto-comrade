#region

using System;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore.Validation;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Usecase
{
    public class AirplaneEditarUsecase : Service
    {
        private readonly AirplaneValidarEditar _airplaneValidarEditar;
        private readonly IAirplaneRepository _repository;

        public AirplaneEditarUsecase(IAirplaneRepository repository, AirplaneValidarEditar airplaneValidarEditar,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneValidarEditar = airplaneValidarEditar;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            try
            {
                var isValid = ValidarEntidade(entity);
                if (!isValid.Sucesso)
                {
                    return isValid;
                }

                var result = await _airplaneValidarEditar.Execute(entity);
                if (!result.Sucesso) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                var sucesso = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<Airplane>(ex);
            }

            return new EditarResult<Airplane>();
        }

        private void HydrateValues(Airplane target, Airplane source)
        {
            target.Codigo = source.Codigo;
            target.QuantidadePassageiro = source.QuantidadePassageiro;
            target.Modelo = source.Modelo;
        }
    }
}
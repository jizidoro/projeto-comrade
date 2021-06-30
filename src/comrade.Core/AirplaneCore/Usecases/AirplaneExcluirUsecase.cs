#region

using System;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore.Validations;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Usecases
{
    public class AirplaneExcluirUsecase : Service
    {
        private readonly AirplaneValidarExcluir _airplaneValidarExcluir;
        private readonly IAirplaneRepository _repository;

        public AirplaneExcluirUsecase(IAirplaneRepository repository, AirplaneValidarExcluir airplaneValidarExcluir,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneValidarExcluir = airplaneValidarExcluir;
        }

        public async Task<ISingleResult<Airplane>> Execute(int id)
        {
            try
            {
                var validacao = await _airplaneValidarExcluir.Execute(id);
                if (!validacao.Sucesso) return validacao;

                _repository.Remove(id);

                var sucesso = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<Airplane>(MensagensNegocio.MSG07);
            }

            return new ExcluirResult<Airplane>();
        }
    }
}
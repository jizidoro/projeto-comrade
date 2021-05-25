#region

using System;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore.Validation;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Usecase
{
    public class AirplaneIncluirUsecase : Service
    {
        private readonly AirplaneValidarIncluir _airplaneValidarIncluir;
        private readonly IAirplaneRepository _repository;

        public AirplaneIncluirUsecase(IAirplaneRepository repository, AirplaneValidarIncluir airplaneValidarIncluir,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneValidarIncluir = airplaneValidarIncluir;
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

                var validacao = await _airplaneValidarIncluir.Execute(entity);
                if (!validacao.Sucesso) return validacao;
                entity.DataRegistro = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
                await _repository.Add(entity);

                var sucesso = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<Airplane>(MensagensNegocio.MSG07);
            }

            return new IncluirResult<Airplane>(entity);
        }
    }
}
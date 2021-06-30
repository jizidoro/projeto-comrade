#region

using AutoMapper;
using comrade.Application.Services;
using comrade.Core.AirplaneCore.Usecases;
using comrade.Core.AirplaneCore.Validations;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;

#endregion

namespace comrade.UnitTests.Tests.AirplaneTests.Bases
{
    public sealed class AirplaneInjectionAppService
    {
        public AirplaneAppService ObterAirplaneAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var airplaneRepository = new AirplaneRepository(context);

            var airplaneValidarCodigoRepetido = new AirplaneValidarCodigoRepetido(airplaneRepository);


            var airplaneValidarEditar =
                new AirplaneValidarEditar(airplaneRepository, airplaneValidarCodigoRepetido);
            var airplaneValidarExcluir = new AirplaneValidarExcluir(airplaneRepository);
            var airplaneValidarIncluir =
                new AirplaneValidarIncluir(airplaneRepository, airplaneValidarCodigoRepetido);
            var airplaneIncluirUsecase = new AirplaneIncluirUsecase(airplaneRepository, airplaneValidarIncluir, uow);
            var airplaneExcluirUsecase = new AirplaneExcluirUsecase(airplaneRepository, airplaneValidarExcluir, uow);
            var airplaneEditarUsecase = new AirplaneEditarUsecase(airplaneRepository, airplaneValidarEditar, uow);

            return new AirplaneAppService(airplaneRepository, airplaneEditarUsecase, airplaneIncluirUsecase,
                airplaneExcluirUsecase, mapper);
        }
    }
}
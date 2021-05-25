#region

using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.WebApi.UseCases.V1.AirplaneApi;

#endregion

namespace comrade.UnitTests.Tests.AirplaneTests.Bases
{
    public class AirplaneInjectionController
    {
        private readonly AirplaneInjectionAppService _airplaneInjectionAppService = new();

        public AirplaneController ObterAirplaneController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();

            var airplaneAppService = _airplaneInjectionAppService.ObterAirplaneAppService(context, mapper);

            return new AirplaneController(airplaneAppService, mapper);
        }
    }
}
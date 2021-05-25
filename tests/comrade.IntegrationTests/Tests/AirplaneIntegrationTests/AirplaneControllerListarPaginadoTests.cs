#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Queries;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerListarPaginadoTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Listar_Paginado()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_return_AirplaneController_Listar_Paginado")
                .Options;


            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.ObterAirplaneController(context);
            var paginationQuery = new PaginationQuery();
            var result = await airplaneController.Listar(paginationQuery);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as PageResultDto<AirplaneDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Codigo);
                Assert.NotNull(actualResultValue.Data);
                Assert.Equal(3, actualResultValue.Data.Count);
            }
        }
    }
}
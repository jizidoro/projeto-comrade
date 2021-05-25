#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UsuarioSistemaControllerObterTests
    {
        private readonly UsuarioSistemaInjectionController _usuarioSistemaInjectionController = new();

        [Fact]
        public async Task UsuarioSistemaController_Obter()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_Obter_usuario_sistema_Controller")
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var usuarioSistemaController = _usuarioSistemaInjectionController.ObterUsuarioSistemaController(context);
            var result = await usuarioSistemaController.Obter(1);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<UsuarioSistemaDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Codigo);
                Assert.NotNull(actualResultValue.Data);
            }
        }
    }
}
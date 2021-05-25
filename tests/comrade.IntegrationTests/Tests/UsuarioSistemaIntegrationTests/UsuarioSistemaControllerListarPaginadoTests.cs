#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Application.Queries;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UsuarioSistemaControllerListarPaginadoTests
    {
        private readonly UsuarioSistemaInjectionController _usuarioSistemaInjectionController = new();

        [Fact]
        public async Task UsuarioSistemaController_Listar_Paginado()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_Listar_usuario_sistema_Paginado")
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var usuarioSistemaController = _usuarioSistemaInjectionController.ObterUsuarioSistemaController(context);
            var pagination = new PaginationQuery(1, 3);
            var result = await usuarioSistemaController.Listar(pagination);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as PageResultDto<UsuarioSistemaDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Codigo);
                Assert.NotNull(actualResultValue.Data);
                Assert.Equal(3, actualResultValue.Data.Count);
            }
        }
    }
}
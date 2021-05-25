#region

using System.Threading.Tasks;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UsuarioSistemaContextTests
    {
        private readonly UsuarioSistemaInjectionController _usuarioSistemaInjectionController = new();

        [Fact]
        public async Task UsuarioSistema_Context()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_obter_usuario_sistema_Respositorio")
                .Options;

            UsuarioSistema usuarioSistema = null;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var repository = new UsuarioSistemaRepository(context);
            usuarioSistema = await repository.GetById(1);
            Assert.NotNull(usuarioSistema);
        }
    }
}
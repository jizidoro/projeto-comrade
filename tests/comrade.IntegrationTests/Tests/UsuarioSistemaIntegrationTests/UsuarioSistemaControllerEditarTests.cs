#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UsuarioSistemaControllerEditarTests
    {
        private readonly UsuarioSistemaInjectionController _usuarioSistemaInjectionController = new();

        [Fact]
        public async Task UsuarioSistemaController_Editar()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_editar_usuario_sistema")
                .Options;

            var alteracaoNome = "Novo Nome";
            var alteracaoEmail = "novo@email.com";
            var alteracaoSenha = "NovaSenha";
            var alteracaoMatricula = "NovaMatricula";

            var teste = new UsuarioSistemaEditarDto
            {
                Id = 1,
                Nome = alteracaoNome,
                Email = alteracaoEmail,
                Senha = alteracaoSenha,
                Situacao = false,
                Matricula = alteracaoMatricula
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var usuarioSistemaController = _usuarioSistemaInjectionController.ObterUsuarioSistemaController(context);
            var result = await usuarioSistemaController.Editar(teste);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Codigo);
            }

            var repository = new UsuarioSistemaRepository(context);
            var usuario = await repository.GetById(1);
            Assert.Equal(alteracaoNome, usuario.Nome);
            Assert.Equal(alteracaoEmail, usuario.Email);
            // Assert.Equal(alteracaoSenha, usuario.Senha);
            Assert.Equal(alteracaoMatricula, usuario.Matricula);
            Assert.False(usuario.Situacao);
        }

        [Fact]
        public async Task Editar_UsuarioSistema_Erro()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_editar_usuario_sistema_Erro")
                .Options;

            var alteracaoNome = "Novo Nome";
            var alteracaoEmail = "novo@email.com";
            var alteracaoMatricula = "NovaMatricula";

            var teste = new UsuarioSistemaEditarDto
            {
                Id = 1,
                Nome = alteracaoNome,
                Situacao = false
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var usuarioSistemaController = _usuarioSistemaInjectionController.ObterUsuarioSistemaController(context);
            var result = await usuarioSistemaController.Editar(teste);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Codigo);
            }

            var repository = new UsuarioSistemaRepository(context);
            var usuario = await repository.GetById(1);
            Assert.NotEqual(alteracaoNome, usuario.Nome);
            Assert.NotEqual(alteracaoEmail, usuario.Email);
            Assert.NotEqual(alteracaoMatricula, usuario.Matricula);
            Assert.True(usuario.Situacao);
        }
    }
}
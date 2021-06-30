#region

using System.Threading.Tasks;
using comrade.Domain.Extensions;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AutenticacaoTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.UnitTests.Tests.AutenticacaoTests
{
    public sealed class AtualizarSenhaExpiradaUsecaseTests
    {
        private readonly AutenticacaoInjectionUseCase _autenticacaoInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public AtualizarSenhaExpiradaUsecaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Test_AtualizarSenhaExpiradaUsecase()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_atualizar_senha_expirada_usecase")
                .Options;


            var teste = new UsuarioSistema
            {
                Id = 1,
                Nome = "111",
                Email = "777@teste",
                Senha = "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=",
                Situacao = true,
                Matricula = "123",
                DataRegistro = DateTimeBrasilia.GetDateTimeBrasilia()
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var repository = new UsuarioSistemaRepository(context);
            var retornoAntes = await repository.GetById(teste.Id);
            var senhaAntes = retornoAntes.Senha;

            var atualizarSenhaExpiradaUsecase =
                _autenticacaoInjectionUseCase.ObterAtualizarSenhaExpiradaUsecase(context);
            var result = await atualizarSenhaExpiradaUsecase.Execute(teste);
            _output.WriteLine(result.Mensagem);

            var retornoDepois = await repository.GetById(teste.Id);
            var senhaDepois = retornoDepois.Senha;

            Assert.NotEqual(senhaAntes, senhaDepois);
        }
    }
}
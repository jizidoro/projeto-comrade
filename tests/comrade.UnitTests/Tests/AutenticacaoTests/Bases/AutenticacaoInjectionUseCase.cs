#region

using System.Collections.Generic;
using comrade.Application.Services;
using comrade.Core.Helpers.Extensions;
using comrade.Core.Helpers.Models;
using comrade.Core.SecurityCore.Usecase;
using comrade.Core.SecurityCore.Validation;
using comrade.Core.UsuarioSistemaCore.Validation;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.Infrastructure.Repositories.Views;
using comrade.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;

#endregion

namespace comrade.UnitTests.Tests.AutenticacaoTests.Bases
{
    public sealed class AutenticacaoInjectionUseCase
    {
        public AtualizarSenhaExpiradaUsecase ObterAtualizarSenhaExpiradaUsecase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);

            var usuarioSistemaCoreValidarEditar =
                new UsuarioSistemaValidarEditar(usuarioSistemaCoreRepository
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new AtualizarSenhaExpiradaUsecase(usuarioSistemaCoreRepository,
                usuarioSistemaCoreValidarEditar, passwordHasher, uow);
        }

        public EsquecerSenhaUsecase ObterEsquecerSenhaUsecase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);
            var usuarioSistemaValidarEditar = new UsuarioSistemaValidarEditar(usuarioSistemaCoreRepository);
            var usuarioSistemaValidarEsquecerSenha =
                new UsuarioSistemaValidarEsquecerSenha(usuarioSistemaCoreRepository, usuarioSistemaValidarEditar
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new EsquecerSenhaUsecase(usuarioSistemaCoreRepository, usuarioSistemaValidarEsquecerSenha,
                passwordHasher, uow);
        }

        public GerarTokenLoginUsecase ObterGerarTokenLoginUsecase(ComradeContext context)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);

            var passwordHasher = new PasswordHasher(new HashingOptions());

            var usuarioSistemaValidarSenha =
                new UsuarioSistemaValidarSenha(usuarioSistemaCoreRepository, passwordHasher);
            var vUsuarioSistemaPermissaoRepository =
                new VwUsuarioSistemaPermissaoRepository(context);
            var gerarTokenLoginUsecase =
                new GerarTokenLoginUsecase
                (
                    configuration,
                    usuarioSistemaValidarSenha
                );
            return gerarTokenLoginUsecase;
        }

        private AutenticacaoAppService ObterUsuarioSistemaAppService(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var vUsuarioSistemaRepository = new VwUsuarioSistemaPermissaoRepository(context);
            var mapper = MapperHelper.ConfigMapper();

            var oterAtualizarSenhaExpiradaUsecase = ObterAtualizarSenhaExpiradaUsecase(context);
            var obterEsquecerSenhaUsecase = ObterEsquecerSenhaUsecase(context);
            var obterGerarTokenLoginUsecaseUsecase = ObterGerarTokenLoginUsecase(context);

            var autenticacaoAppService = new AutenticacaoAppService(vUsuarioSistemaRepository,
                oterAtualizarSenhaExpiradaUsecase,
                obterGerarTokenLoginUsecaseUsecase, obterEsquecerSenhaUsecase, mapper);
            return autenticacaoAppService;
        }
    }
}
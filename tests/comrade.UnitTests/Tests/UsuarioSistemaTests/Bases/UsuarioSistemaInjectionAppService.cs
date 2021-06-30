#region

using AutoMapper;
using comrade.Application.Services;
using comrade.Core.Helpers.Models;
using comrade.Core.UsuarioSistemaCore.Usecases;
using comrade.Core.UsuarioSistemaCore.Validations;
using comrade.Domain.Extensions;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;

#endregion

namespace comrade.UnitTests.Tests.UsuarioSistemaTests.Bases
{
    public sealed class UsuarioSistemaInjectionAppService
    {
        public UsuarioSistemaAppService ObterUsuarioSistemaAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaRepository = new UsuarioSistemaRepository(context);
            var passwordHasher = new PasswordHasher(new HashingOptions());

            var usuarioSistemaValidarEditar =
                new UsuarioSistemaValidarEditar(usuarioSistemaRepository);
            var usuarioSistemaValidarExcluir = new UsuarioSistemaValidarExcluir(usuarioSistemaRepository);
            var usuarioSistemaValidarIncluir =
                new UsuarioSistemaValidarIncluir(usuarioSistemaRepository);
            var usuarioSistemaIncluirUsecase =
                new UsuarioSistemaIncluirUsecase(usuarioSistemaRepository, usuarioSistemaValidarIncluir, passwordHasher,
                    uow);
            var usuarioSistemaExcluirUsecase =
                new UsuarioSistemaExcluirUsecase(usuarioSistemaRepository, usuarioSistemaValidarExcluir, uow);
            var usuarioSistemaEditarUsecase =
                new UsuarioSistemaEditarUsecase(usuarioSistemaRepository, usuarioSistemaValidarEditar, uow);

            return new UsuarioSistemaAppService(usuarioSistemaRepository, usuarioSistemaEditarUsecase,
                usuarioSistemaIncluirUsecase,
                usuarioSistemaExcluirUsecase, mapper);
        }
    }
}
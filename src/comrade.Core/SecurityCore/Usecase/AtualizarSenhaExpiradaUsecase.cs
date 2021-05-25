#region

using System;
using System.Threading.Tasks;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Extensions;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.UsuarioSistemaCore;
using comrade.Core.UsuarioSistemaCore.Validation;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.SecurityCore.Usecase
{
    public class AtualizarSenhaExpiradaUsecase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsuarioSistemaRepository _repository;
        private readonly UsuarioSistemaValidarEditar _usuarioSistemaValidarEditar;

        public AtualizarSenhaExpiradaUsecase(IUsuarioSistemaRepository repository,
            UsuarioSistemaValidarEditar usuarioSistemaValidarEditar,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _usuarioSistemaValidarEditar = usuarioSistemaValidarEditar;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<UsuarioSistema>> Execute(UsuarioSistema entity)
        {
            try
            {
                var result = await _usuarioSistemaValidarEditar.Execute(entity);
                if (!result.Sucesso) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                var sucesso = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<UsuarioSistema>(ex);
            }

            return new EditarResult<UsuarioSistema>();
        }

        private void HydrateValues(UsuarioSistema target, UsuarioSistema source)
        {
            target.Senha = _passwordHasher.Hash(source.Senha);
        }
    }
}
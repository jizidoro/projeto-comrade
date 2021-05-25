#region

using System;
using System.Threading.Tasks;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.UsuarioSistemaCore.Validation;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UsuarioSistemaCore.Usecase
{
    public class UsuarioSistemaExcluirUsecase : Service
    {
        private readonly IUsuarioSistemaRepository _repository;
        private readonly UsuarioSistemaValidarExcluir _usuarioSistemaValidarExcluir;

        public UsuarioSistemaExcluirUsecase(IUsuarioSistemaRepository repository,
            UsuarioSistemaValidarExcluir usuarioSistemaValidarExcluir,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _usuarioSistemaValidarExcluir = usuarioSistemaValidarExcluir;
        }

        public async Task<ISingleResult<UsuarioSistema>> Execute(int id)
        {
            try
            {
                var validacao = await _usuarioSistemaValidarExcluir.Execute(id);
                if (!validacao.Sucesso) return validacao;

                _repository.Remove(id);

                var sucesso = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<UsuarioSistema>(MensagensNegocio.MSG07);
            }

            return new ExcluirResult<UsuarioSistema>();
        }
    }
}
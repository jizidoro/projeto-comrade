#region

using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UsuarioSistemaCore.Validations
{
    public class UsuarioSistemaValidarIncluir : EntityValidation<UsuarioSistema>
    {
        private readonly IUsuarioSistemaRepository _repository;

        public UsuarioSistemaValidarIncluir(IUsuarioSistemaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ISingleResult<UsuarioSistema> Execute(UsuarioSistema entity)
        {
            return new SingleResult<UsuarioSistema>(entity);
        }
    }
}
#region

using comrade.Core.Helpers.Extensions;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.Helpers.Models.Validations;
using comrade.Core.UsuarioSistemaCore;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.SecurityCore.Validation
{
    public class UsuarioSistemaValidarSenha : EntityValidation<UsuarioSistema>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;

        public UsuarioSistemaValidarSenha(IUsuarioSistemaRepository usuarioSistemaRepository,
            IPasswordHasher passwordHasher)
            : base(usuarioSistemaRepository)
        {
            _usuarioSistemaRepository = usuarioSistemaRepository;
            _passwordHasher = passwordHasher;
        }

        public ISingleResult<UsuarioSistema> Execute(int chave, string senha)
        {
            var usuSelecionado = _usuarioSistemaRepository.GetById(chave).Result;
            var chaveValida = usuSelecionado != null;

            if (chaveValida)
            {
                var senhaValida = _passwordHasher.Check(usuSelecionado.Senha, senha);

                if (!senhaValida.Verified)
                {
                    return new SingleResult<UsuarioSistema>(1001, "Usuário ou senha informados não são válidos");
                }


                return new SingleResult<UsuarioSistema>(usuSelecionado);
            }


            return new SingleResult<UsuarioSistema>(1001, "Usuário ou senha informados não são válidos");
        }
    }
}
#region

using comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace comrade.Application.Validations.BaUsuValitation
{
    public class UsuarioSistemaIncluirValidation : UsuarioSistemaValidation<UsuarioSistemaIncluirDto>
    {
        public UsuarioSistemaIncluirValidation()
        {
            ValidarNome();
            ValidarEmail();
            ValidarSenha();
            ValidarMatricula();
        }
    }
}
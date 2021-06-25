#region

using comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace comrade.Application.Validations.UsuarioSistemaValidations
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
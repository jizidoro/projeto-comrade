#region

using comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace comrade.Application.Validations.UsuarioSistemaValidations
{
    public class UsuarioSistemaEditarValidation : UsuarioSistemaValidation<UsuarioSistemaEditarDto>
    {
        public UsuarioSistemaEditarValidation()
        {
            ValidarId();
            ValidarNome();
            ValidarEmail();
            ValidarSenha();
            ValidarMatricula();
        }
    }
}
#region

using comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace comrade.Application.Validations.UsuarioSistemaValidations
{
    public class UsuarioSistemaExcluirValidation : UsuarioSistemaValidation<UsuarioSistemaDto>
    {
        public UsuarioSistemaExcluirValidation()
        {
            ValidarId();
        }
    }
}
#region

using comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace comrade.Application.Validations.BaUsuValitation
{
    public class UsuarioSistemaExcluirValidation : UsuarioSistemaValidation<UsuarioSistemaDto>
    {
        public UsuarioSistemaExcluirValidation()
        {
            ValidarId();
        }
    }
}
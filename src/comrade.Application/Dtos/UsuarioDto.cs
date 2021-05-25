#region

using comrade.Application.Bases;

#endregion

namespace comrade.Application.Dtos
{
    public class UsuarioDto : EntityDto
    {
        public string Token { get; set; }
    }
}
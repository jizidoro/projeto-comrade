#region

using comrade.Application.Bases;

#endregion

namespace comrade.Application.Dtos
{
    public class AutenticacaoDto : EntityDto
    {
        public string Chave { get; set; }
        public string Senha { get; set; }
    }
}
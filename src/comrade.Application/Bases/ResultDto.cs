#region

using System.Collections.Generic;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Bases
{
    public class ResultDto : IResultDto
    {
        public int Codigo { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public IList<string> Mensagens { get; set; }
    }
}
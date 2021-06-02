#region

using System.Collections.Generic;
using comrade.External.Utils;

#endregion

namespace comrade.External.Bases
{
    public class ResultDto
    {
        public int Codigo { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public IList<string> Mensagens { get; set; }
    }
}
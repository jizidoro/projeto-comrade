#region

using System.Collections.Generic;

#endregion

namespace comrade.Core.Utils
{
    public class User
    {
        public string Chave { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public IList<string> Papeis { get; set; }
    }
}
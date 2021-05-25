#region

using comrade.Domain.Enums;

#endregion

namespace comrade.Core.Utils
{
    public class SecurityResult
    {
        public SecurityResult(User user)
        {
            User = user;
            Code = (int) EnumResultadoAcao.Sucesso;
            Success = true;
        }

        public SecurityResult(int code, string message)
        {
            Code = code;
            ErrorMessage = message;
            Success = false;
        }

        public User User { get; set; }
        public bool Success { get; set; }
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Mensagem { get; set; }
    }
}
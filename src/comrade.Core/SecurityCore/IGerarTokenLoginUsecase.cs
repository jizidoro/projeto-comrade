#region

using System.Threading.Tasks;
using comrade.Core.Utils;

#endregion

namespace comrade.Core.SecurityCore
{
    public interface IGerarTokenLoginUsecase
    {
        Task<SecurityResult> Execute(string chave, string senha);
    }
}
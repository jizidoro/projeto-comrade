#region

using System.Linq;
using comrade.Domain.Models.Views;

#endregion

namespace comrade.Core.Views.VBaUsuPermissaoCore
{
    public interface IVwUsuarioSistemaPermissaoRepository
    {
        IQueryable<VwUsuarioSistemaPermissao> ListarPorSqUsuarioSistema(int sqUsuarioSistema);
    }
}
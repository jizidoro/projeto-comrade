#region

using System.Linq;
using comrade.Core.Helpers.Interfaces;
using comrade.Domain.Bases;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UsuarioSistemaCore
{
    public interface IUsuarioSistemaRepository : IRepository<UsuarioSistema>
    {
        IQueryable<LookupEntity> BuscarPorNome(string nome);
    }
}
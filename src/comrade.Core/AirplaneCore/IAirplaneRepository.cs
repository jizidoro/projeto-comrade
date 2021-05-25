#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore
{
    public interface IAirplaneRepository : IRepository<Airplane>
    {
        Task<ISingleResult<Airplane>> RegistroCodigoRepetido(int id, string codigo);
    }
}
#region

using System.Threading.Tasks;

#endregion

namespace comrade.Core.Helpers.Interfaces
{
    public interface IService
    {
        Task<bool> Commit();
    }
}
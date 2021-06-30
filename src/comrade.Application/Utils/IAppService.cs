#region

using AutoMapper;
using comrade.Core.Helpers.Interfaces;
using comrade.Domain.Bases;

#endregion

namespace comrade.Application.Utils
{
    public interface IAppService
    {
        IMapper Mapper { get; }
    }
}
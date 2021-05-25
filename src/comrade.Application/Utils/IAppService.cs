#region

using AutoMapper;

#endregion

namespace comrade.Application.Utils
{
    public interface IAppService
    {
        IMapper Mapper { get; }
    }
}
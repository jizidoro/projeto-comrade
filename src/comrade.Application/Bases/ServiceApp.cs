#region

using AutoMapper;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Bases
{
    public class AppService : IAppService
    {
        public AppService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }
    }
}
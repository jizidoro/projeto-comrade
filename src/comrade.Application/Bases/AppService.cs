#region

using AutoMapper;
using comrade.Application.Utils;
using comrade.Core.Helpers.Interfaces;
using comrade.Domain.Bases;

#endregion

namespace comrade.Application.Bases
{
    public class AppService
    {
        public AppService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

    }
}
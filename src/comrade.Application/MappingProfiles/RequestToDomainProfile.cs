#region

using AutoMapper;
using comrade.Application.Filters;
using comrade.Application.Queries;

#endregion

namespace comrade.Application.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
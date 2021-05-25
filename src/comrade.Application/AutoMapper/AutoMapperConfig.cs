#region

using AutoMapper;
using comrade.Application.MappingProfiles;

#endregion

namespace comrade.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new(cfg =>
            {
                cfg.AddProfile(new DomainToDtoMappingProfile());
                cfg.AddProfile(new DtoToDomainMappingProfile());
                cfg.AddProfile(new RequestToDomainProfile());
            });
        }
    }
}
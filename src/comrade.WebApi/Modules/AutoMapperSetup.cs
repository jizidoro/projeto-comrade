#region

using System;
using comrade.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace comrade.WebApi.Modules
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
        }
    }
}
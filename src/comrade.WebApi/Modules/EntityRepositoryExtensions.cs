#region

using comrade.Core.AirplaneCore;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.UsuarioSistemaCore;
using comrade.Core.Views.VBaUsuPermissaoCore;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.Infrastructure.Repositories.Views;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

#endregion

namespace comrade.WebApi.Modules
{
    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class EntityRepositoryExtensions
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddEntityRepository(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IFeatureManager featureManager = services
                .BuildServiceProvider()
                .GetRequiredService<IFeatureManager>();

            var isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.SqlServer))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (isEnabled)
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IAirplaneRepository, AirplaneRepository>();
                services.AddScoped<IUsuarioSistemaRepository, UsuarioSistemaRepository>();
                services.AddScoped<IVwUsuarioSistemaPermissaoRepository, VwUsuarioSistemaPermissaoRepository>();
            }

            return services;
        }
    }
}
#region

using comrade.Infrastructure.DataAccess;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

#endregion

namespace comrade.WebApi.Modules
{
    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class SqlServerExtensionsFake
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddSqlServerFake(
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
                services.AddDbContext<ComradeContext>(options => options.UseInMemoryDatabase("test_database"));
            }

            return services;
        }
    }
}
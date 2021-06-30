#region

using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using comrade.Domain.Extensions;
using comrade.Infrastructure.DataAccess;
using comrade.WebApi.Modules.Common.FeatureFlags;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;

#endregion

namespace comrade.WebApi.Modules
{
    /// <summary>
    ///     HealthChecks Extensions.
    /// </summary>
    public static class HealthChecksExtensions
    {
        /// <summary>
        ///     Add Health Checks dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IHealthChecksBuilder healthChecks = services.AddHealthChecks();

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
                services.AddHealthChecks()
                    .AddDbContextCheck<ComradeContext>("ComradeContext")
                    .AddApplicationInsightsPublisher();

                services.AddHealthChecksUI()
                    .AddInMemoryStorage();

                healthChecks.AddSqlServer(configuration.GetValue<string>("PersistenceModule:DefaultConnection"),
                    name: "sqlserver", tags: new string[] {"db", "data"});
            }


            return services;
        }

        /// <summary>
        ///     Use Health Checks dependencies.
        /// </summary>
        public static IApplicationBuilder UseHealthChecks(
            this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health",
                new HealthCheckOptions {ResponseWriter = WriteResponse});

            app.UseHealthChecks("/healthcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
                options.ApiPath = "/monitor-api";
            });

            return app;
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            var teste = JsonSerializer.Serialize(
                new
                {
                    currentTime = DateTimeBrasilia.GetDateTimeBrasilia(),
                    statusApplication = result.Status.ToString(),
                    healthChecks = result.Entries.Select(e => new
                    {
                        check = e.Key,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                    })
                });

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(teste);
        }
    }
}
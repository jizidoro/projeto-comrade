#region

using Microsoft.AspNetCore.Builder;
using Prometheus;

#endregion

namespace comrade.WebApi.Modules.Common
{
    /// <summary>
    ///     Http Metrics Extensions.
    /// </summary>
    public static class HttpMetricsExtensions
    {
        /// <summary>
        ///     Add Prometheus dependencies.
        /// </summary>
        public static IApplicationBuilder UseCustomHttpMetrics(this IApplicationBuilder appBuilder)
        {
            return appBuilder
                .UseMetricServer()
                .UseHttpMetrics(options =>
                {
                    options.RequestDuration.Enabled = true;
                    options.InProgress.Enabled = true;
                    options.RequestCount.Enabled = true;
                });
        }
    }
}
#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

#endregion

namespace comrade.WebApi.Modules.Common
{
    /// <summary>
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInvalidRequestLogging(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var logger = actionContext
                        .HttpContext
                        .RequestServices
                        .GetRequiredService<ILogger<Startup>>();

                    List<string> errors = actionContext.ModelState
                        .Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    string jsonModelState = JsonSerializer.Serialize(errors);
                    logger.LogWarning("Invalid request.", jsonModelState);

                    ValidationProblemDetails problemDetails = new(actionContext.ModelState);
                    return new BadRequestObjectResult(problemDetails);
                };
            });

            return services;
        }

        public static void CreateLogMongoDb(LoggerProviderCollection providers, IConfigurationRoot configurationRoot)
        {
            var connection = configurationRoot.GetValue<string>("ConnectionStrings:DefaultConnection");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new ApplicationDetailsEnricher())
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDB("mongodb://localhost/local")
                .WriteTo.Providers(providers)
                .CreateLogger();
        }

        public static void CreateLogSqlServer(LoggerProviderCollection providers, IConfigurationRoot configurationRoot)
        {
            var connection = configurationRoot.GetValue<string>("ConnectionStrings:DefaultConnection");

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("UserName", System.Data.SqlDbType.VarChar)
                }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new ApplicationDetailsEnricher())
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connection,
                    sinkOptions: new MSSqlServerSinkOptions()
                    {
                        AutoCreateSqlTable = true,
                        TableName = "LogAPIContagem"
                    }, columnOptions: columnOptions)
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDB("mongodb://localhost/local")
                .WriteTo.Providers(providers)
                .CreateLogger();
        }
    }
}
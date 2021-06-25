#region

using System;
using comrade.WebApi.Modules;
using comrade.WebApi.Modules.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using Serilog;
using Serilog.Extensions.Logging;

#endregion

namespace comrade.WebApi
{
    /// <summary>
    /// </summary>
    public static class Program
    {
        private static readonly LoggerProviderCollection Providers = new();

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var hostBuilder = CreateHostBuilder(args).Build();

            try
            {
                Log.Information("Starting up");
                hostBuilder.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddCommandLine(args);
                    var settings = configApp.Build();
                    LoggingExtensions.CreateLogMongoDb(Providers, settings);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog(providers: Providers);
        }
    }
}
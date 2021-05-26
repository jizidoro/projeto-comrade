#region

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

#endregion

namespace comrade.WebApi
{
    /// <summary>
    /// </summary>
    public static class Program
    {
        static readonly LoggerProviderCollection Providers = new LoggerProviderCollection();

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDB("mongodb://localhost/local", collectionName: "log")
                .WriteTo.Providers(Providers)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) => { configApp.AddCommandLine(args); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog(providers: Providers);
        }
    }
}
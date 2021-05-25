#region

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

#endregion

namespace comrade.Infrastructure.DataAccess
{
    /// <summary>
    ///     ContextFactory.
    /// </summary>
    public sealed class ContextFactory : IDesignTimeDbContextFactory<ComradeContext>
    {
        /// <summary>
        ///     Instantiate a ComradeContext.
        /// </summary>
        /// <param name="args">Command line args.</param>
        public ComradeContext CreateDbContext(string[] args)
        {
            var connectionString = ReadDefaultConnectionStringFromAppSettings();

            var builder = new DbContextOptionsBuilder<ComradeContext>();
            Console.WriteLine(connectionString);
            builder.UseSqlServer(connectionString);
            builder.EnableSensitiveDataLogging();
            return new ComradeContext(builder.Options);
        }

        private static string ReadDefaultConnectionStringFromAppSettings()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{envName}.json", false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetValue<string>("PersistenceModule:DefaultConnection");
            return connectionString;
        }
    }
}
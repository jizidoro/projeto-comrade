#region

using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace comrade.WebApi.Modules.Common
{
    /// <summary>
    ///     Data Protection Extensions.
    /// </summary>
    public static class DataProtectionExtensions
    {
        /// <summary>
        ///     Add Data Protection.
        /// </summary>
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services)
        {
            services.AddDataProtection()
                .SetApplicationName("airplanes-api")
                .PersistKeysToFileSystem(new DirectoryInfo(@"./"));

            return services;
        }
    }
}
#region

using comrade.Application.Interfaces;
using comrade.Application.Services;
using comrade.Core.Helpers.Interfaces;
using comrade.Infrastructure.Bases;
using comrade.WebApi.Modules;
using comrade.WebApi.Modules.Common;
using comrade.WebApi.Modules.Common.FeatureFlags;
using comrade.WebApi.Modules.Common.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

#endregion

namespace comrade.UnitTests.Helpers
{
    public class ObterServiceProviderDb
    {
        public ServiceProvider Execute()
        {
            var services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            services
                .AddFeatureFlags(configuration)
                .AddInvalidRequestLogging()
                .AddSqlServer(configuration)
                .AddEntityRepository(configuration)
                .AddHealthChecks(configuration)
                .AddAuthentication(configuration)
                .AddVersioning()
                .AddSwagger()
                .AddUseCases()
                .AddCustomControllers()
                .AddCustomCors()
                .AddProxy()
                .AddCustomDataProtection();

            services.AddAutoMapperSetup();

            services.AddLogging();

            services.AddScoped(typeof(ILookupServiceApp<>), typeof(LookupServiceApp<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            return services.BuildServiceProvider();
        }
    }
}
#region

using System.Text;
using comrade.Core.SecurityCore;
using comrade.Core.SecurityCore.Usecase;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace comrade.WebApi.Modules.Common
{
    /// <summary>
    ///     Authentication Extensions.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        ///     Add Authentication Extensions.
        /// </summary>
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IFeatureManager featureManager = services
                .BuildServiceProvider()
                .GetRequiredService<IFeatureManager>();

            var isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.Authentication))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            services.AddScoped<IGerarTokenLoginUsecase, GerarTokenLoginUsecase>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            return services;
        }
    }
}
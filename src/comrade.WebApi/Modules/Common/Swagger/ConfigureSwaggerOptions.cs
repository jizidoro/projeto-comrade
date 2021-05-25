#region

using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace comrade.WebApi.Modules.Common.Swagger
{
    /// <summary>
    ///     Configures the Swagger generation options.
    /// </summary>
    /// <remarks>
    ///     This allows API versioning to define a Swagger document per API version after the
    ///     <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
    /// </remarks>
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private const string UriString = "http://urlaqui.net";

        private const string UriString1 =
            "https://raw.urlaqui.com/README.md";

        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
        /// </summary>
        /// <param name="provider">
        ///     The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
        ///     documents.
        /// </param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            OpenApiInfo info = new()
            {
                Title = "Clean Architecture COMRADE",
                Version = description.ApiVersion.ToString(),
                Description = "",
                Contact = new OpenApiContact {Name = "nome aqui", Email = "email@aqui"},
                TermsOfService = new Uri(UriString),
                License = new OpenApiLicense
                {
                    Name = "Apache License",
                    Url = new Uri(
                        UriString1)
                }
            };

            if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

            return info;
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace DotnetToolset.Settings
{
    public static class OpenApi
    {
        /// <summary>
        /// Gets OpenApiInfo from assembly and appsettings*.json
        /// </summary>
        /// <param name="assembly">API startup assembly</param>
        /// <param name="configuration">API configuration settings</param>
        /// <returns></returns>
        public static (OpenApiInfo, string) GetOpenApiInfo(Assembly assembly, IConfiguration configuration)
        {
            OpenApiInfo openApiInfo = configuration.GetSection("OpenApi").Get<OpenApiInfo>();
            openApiInfo.Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            return (openApiInfo, Path.GetDirectoryName(assembly.Location));
        }

        /// <summary>
        /// Creates and returns an opinionated SwaggerGenOptions object
        /// </summary>
        /// <param name="openApiInfo">OpenApiInfo object</param>
        /// <param name="assemblyPath">Path to API startup assembly directory</param>
        /// <returns></returns>
        public static Action<SwaggerGenOptions> GetSwaggerGenOptions(OpenApiInfo openApiInfo, string assemblyPath)
        {
            void SwaggerGenOptionsAction(SwaggerGenOptions options)
            {
                // Add Swagger JSON options
                options.SwaggerDoc($"v{openApiInfo.Version}", openApiInfo);
                options.SwaggerDoc("latest", openApiInfo);

                // Add the XML documentation into Swagger
                options.IncludeXmlComments(Path.Combine(assemblyPath ?? string.Empty, "comments.xml"));

                // Security
                OpenApiSecurityScheme openApiSecuritySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                options.AddSecurityDefinition("Bearer", openApiSecuritySchema);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {openApiSecuritySchema, new[] {"Bearer"}}
                });
            }

            return SwaggerGenOptionsAction;
        }

        /// <summary>
        /// Creates and returns an opinionated SwaggerOptions object
        /// </summary>
        /// <returns></returns>
        public static Action<SwaggerOptions> GetSwaggerOptions()
        {
            void SwaggerOptions(SwaggerOptions options)
            {
                // Customize Swagger / OpenAPI JSON endpoint paths
                options.RouteTemplate = "specs/{documentName}/openapi.json";
            }

            return SwaggerOptions;
        }

        /// <summary>
        /// Creates and returns an opinionated SwaggerUiOptions object
        /// </summary>
        /// <param name="openApiInfo">OpenApiInfo object</param>
        /// <returns></returns>
        public static Action<SwaggerUIOptions> GetSwaggerUiOptions(OpenApiInfo openApiInfo)
        {
            void SwaggerUiOptions(SwaggerUIOptions options)
            {
                // Show Swagger UI at the root path URL. It shows at /swagger by default.
                options.RoutePrefix = "docs";

                // Generate Swagger UI based on the JSON endpoint
                options.SwaggerEndpoint($"/specs/v{openApiInfo.Version}/openapi.json", $"{openApiInfo.Title} v{openApiInfo.Version}");
                options.InjectStylesheet("/assets/openapi.css");
            }

            return SwaggerUiOptions;
        }
    }
}

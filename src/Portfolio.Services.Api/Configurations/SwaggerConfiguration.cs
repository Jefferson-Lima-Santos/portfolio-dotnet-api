﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Portfolio.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Documentation",
                    Version = "v1.0",
                    Description = ""
                });

                s.ResolveConflictingActions(x => x.First());

                var securitySchemeApiKey = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Insira apenas a API Key",
                    In = ParameterLocation.Header,
                    Name = "X-API-Key"
                };
                s.AddSecurityDefinition("apiKey", securitySchemeApiKey);

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apiKey" }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.RouteTemplate = "api/docs/{documentName}/swagger.json"; });
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "api/docs";
                    options.DefaultModelExpandDepth(2);
                    options.DefaultModelRendering(ModelRendering.Example);
                    options.DefaultModelsExpandDepth(-1);
                    options.DisplayOperationId();
                    options.DisplayRequestDuration();
                    options.DocExpansion(DocExpansion.List);
                    options.EnableDeepLinking();
                    options.EnableFilter();
                    options.MaxDisplayedTags(30);
                    options.ShowExtensions();
                    options.EnableValidator();

                    options.SupportedSubmitMethods(
                        SubmitMethod.Get,
                        SubmitMethod.Post,
                        SubmitMethod.Put,
                        SubmitMethod.Delete
                    );
                });
            return app;
        }

    }
}

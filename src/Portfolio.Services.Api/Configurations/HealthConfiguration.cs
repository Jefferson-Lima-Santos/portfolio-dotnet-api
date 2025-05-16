using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;
using Portfolio.Api.Infra.Data.Context;

namespace Portfolio.Services.Api.Configurations
{
    public static class HealthConfiguration
    {
        public static void AddHealthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!)
                .AddDbContextCheck<PortfolioDbContext>(tags: new[] { "appdbcontext", "ready" })
                .AddAzureBlobStorage(
                    tags: new[] { "AzureBlobStorage" },
                    optionsFactory: sp =>
                    {
                        var blobConfig = configuration.GetSection("AzureBlobStorage");

                        return new AzureBlobStorageHealthCheckOptions
                        {
                            ContainerName = blobConfig["ContainerName"]
                        };
                    }
                );
        }

        public static void UseHealthConfig(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            });
            app.UseHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });
        }

        public static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                var mapping = new Dictionary<string, string>
                {
                    {"sqlserver", "db" },
                    {"PortfolioDbContext", "appdbcontext" },
                    {"azure_blob_storage", "AzureBlobStorage" }
                };

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(mapping[healthReportEntry.Key]);
                    jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Portfolio.Api.Infra.Data.Context;
using Portfolio.Services.Api.Filters;
using Asp.Versioning;
using Portfolio.Api.Infra.Data.Interceptor;

namespace Portfolio.Services.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PortfolioDbContext>((sp, options) => options
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));

            services
                .AddMvcCore(o =>
                {
                    o.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                    o.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter)));
                })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(a => a.SuppressModelStateInvalidFilter = true)
                .AddApiExplorer()
                .AddCors(options =>
                {
                    options.AddPolicy("All",
                        builder =>
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
                })
                .AddFormatterMappings();

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                                  new HeaderApiVersionReader("x-api-version"),
                                  new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.Filters.Add(typeof(ValidateCommandFilter));
            });
        }
        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

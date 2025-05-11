using MediatR;
using Serilog;
using Portfolio.Api.Domain.Data;
using Portfolio.Api.Domain.Mediator;
using Portfolio.Api.Domain.Mediator.Notifications;
using Portfolio.Api.Domain.Repositories;
using Portfolio.Api.Infra.Data.Data;
using Portfolio.Api.Infra.Data.Interceptor;
using Portfolio.Api.Infra.Data.Repositories;
using Portfolio.Services.Api.Configurations.ApiKeyConfig;
using Portfolio.Services.Api.Filters;

namespace Portfolio.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblyMediatrProject = AppDomain.CurrentDomain.Load("Portfolio.Api.Domain");
            services.AddMediatR(o =>
            {
                o.RegisterServicesFromAssembly(assemblyMediatrProject);
                o.AddPipelineValidator(services, assemblyMediatrProject);
            });

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // loggers
            services.AddLogging(builder => builder.AddSerilog());
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();


            // api Key attribute
            services.AddSingleton<ApiKeyAuthorizationFilter>();

            // uow
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // SoftDeleteInterceptor
            services.AddScoped<SoftDeleteInterceptor>();
            
            // ProjectRepository
            services.AddScoped<IProjectReadOnlyRepository, ProjectReadOnlyRepository>();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Portfolio.Services.Api.Helpers.Schemes;

namespace Portfolio.Services.Api.Configurations
{
    public static class AuthConfiguration
    {
        public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthentication()
                .AddApiKeySupport(options => { });
        }
    }
}

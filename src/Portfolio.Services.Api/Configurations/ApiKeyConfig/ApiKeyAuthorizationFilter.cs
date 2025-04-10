using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Services.Api.Configurations.ApiKeyConfig
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "X-API-Key";
        private readonly IConfiguration _configuration;

        public ApiKeyAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];

            if (string.IsNullOrEmpty(apiKey) || apiKey != _configuration["AdminApiKey"])
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolio.Api.Domain.Extensions;
using Portfolio.Api.Domain.Mediator;
using Portfolio.Api.Domain.Mediator.Notifications;
using Portfolio.Api.Domain.Projects.Queries;

namespace Portfolio.Services.Api.Controllers
{
    [ApiVersion("1.0")]
    [Microsoft.AspNetCore.Components.Route("apiv{version:apiVersion}")]
    public class ProjectController : ApiController
    {
        private readonly IMediatorHandler _mediator;

        public ProjectController(
            ILogger<ProjectController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(logger, notifications)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "ApiKey")]
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects([FromQuery] PageFilterModel pageFilterModel,
            CancellationToken cancellationToken)
        {
            var projects = await _mediator.Query(new GetAllProjectsPaginatedQuery(pageFilterModel), cancellationToken);

            var metadata = new
            {
                projects.TotalCount,
                projects.PageSize,
                projects.CurrentPage,
                projects.TotalPages,
                projects.HasPreviousPage,
                projects.HasNextPage,
            };
            
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
            
            return ResponseApi(projects);
        }
    }
}
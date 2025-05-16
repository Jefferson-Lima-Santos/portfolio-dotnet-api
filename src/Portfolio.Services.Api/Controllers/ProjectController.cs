using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolio.Api.Domain.Extensions;
using Portfolio.Api.Domain.Mediator;
using Portfolio.Api.Domain.Mediator.Notifications;
using Portfolio.Api.Domain.Projects.Commands;
using Portfolio.Api.Domain.Projects.Queries;

namespace Portfolio.Services.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
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
        [HttpGet("projects/listed")]
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

        [Authorize(AuthenticationSchemes = "ApiKey")]
        [HttpPost("project/register")]
        public async Task<IActionResult> Register([FromBody] RegisterProjectCommand command, CancellationToken cancellationToken)
        {
            await _mediator.SendCommand(command, cancellationToken);
            return ResponseApi();
        }

    }
}
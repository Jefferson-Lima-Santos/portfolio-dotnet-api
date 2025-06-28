using MediatR;
using Portfolio.CrossCutting.GitHub.GitHub.Projects.DTOs;
using Portfolio.CrossCutting.GitHub.Interface;

namespace Portfolio.Api.Domain.Projects.Queries
{
    public class GetAllGitHubProjectsQuery : IRequest<List<GitHubRepositoryResponse>>
    {
        public string UserName { get; }
        public GetAllGitHubProjectsQuery(String userName)
        {
            UserName = userName;
        }
    }

    public class
        GetAllGitHubProjectsQueryQueryHandler : IRequestHandler<GetAllGitHubProjectsQuery, List<GitHubRepositoryResponse>>
    {
        private readonly IGitHubProjectsService _gitHubProjectsService;

        public GetAllGitHubProjectsQueryQueryHandler(IGitHubProjectsService gitHubProjectsService)
        {
            _gitHubProjectsService = gitHubProjectsService;
        }

        public async Task<List<GitHubRepositoryResponse>> Handle(GetAllGitHubProjectsQuery request,
            CancellationToken cancellationToken)
        {
            return await _gitHubProjectsService.GetUserRepositoriesAsync(request.UserName, cancellationToken);
        }
    }
}
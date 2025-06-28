using Portfolio.CrossCutting.GitHub.GitHub.Projects.DTOs;

namespace Portfolio.CrossCutting.GitHub.Interface
{
    public interface IGitHubProjectsService
    {
        Task<List<GitHubRepositoryResponse>> GetUserRepositoriesAsync(string username, CancellationToken cancellationToken);
    }
}
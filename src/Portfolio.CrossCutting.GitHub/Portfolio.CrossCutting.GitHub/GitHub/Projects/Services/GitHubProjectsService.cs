using Newtonsoft.Json;
using Portfolio.CrossCutting.GitHub.GitHub.Projects.DTOs;
using Portfolio.CrossCutting.GitHub.Interface;

namespace Portfolio.CrossCutting.GitHub.GitHub.Projects.Services;

public class GitHubProjectsService : IGitHubProjectsService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GitHubProjectsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<GitHubRepositoryResponse>> GetUserRepositoriesAsync(string username, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("GitHubClient");
        string url = $"users/{username}/repos";
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        var repositories = JsonConvert.DeserializeObject<List<GitHubRepositoryResponse>>(content);

        if (repositories == null || !repositories.Any())
        {
            return null;
        }

        return repositories.ToList();
        }
}
using Newtonsoft.Json;

namespace Portfolio.CrossCutting.GitHub.GitHub.Projects.DTOs
{
    public class GitHubRepositoryResponse
    {
        [JsonProperty("id")]
        public long ProjectUId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public string GitHubURL { get; set; }

        [JsonProperty("owner")]
        public GitHubOwner Owner { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("stargazers_count")]
        public int Stars { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}

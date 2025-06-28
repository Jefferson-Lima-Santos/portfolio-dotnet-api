using Newtonsoft.Json;

namespace Portfolio.CrossCutting.GitHub.GitHub.Projects.DTOs
{
    public class GitHubOwner
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}

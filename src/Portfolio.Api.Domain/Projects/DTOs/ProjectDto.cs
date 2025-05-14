using Newtonsoft.Json;

namespace Portfolio.Api.Domain.Projects.DTOs
{
    public class ProjectDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("ProjectUId")]
        public long ProjectUId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("git_hub_url")]
        public string GitHubURL { get; set; }
        [JsonProperty("images")]
        public IEnumerable<String> Images { get; set; }
    }
}
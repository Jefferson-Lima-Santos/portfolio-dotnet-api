using Portfolio.Api.Domain.Data;


namespace Portfolio.Api.Domain.Entities
{
    public class Project : ISoftDeletable
    {
        public long Id { get; set; }
        public Guid ProjectUId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string GitHubURL { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }

        // Um projeto pode ter várias imagens
        public ICollection<ProjectImages> ProjectImages { get; set; } = new List<ProjectImages>();

        public static class ProjectFactory
        {
            public static Project Create(
                string name,
                string subtitle,
                string description,
                string gitHubURL,
                ICollection<ProjectImages> images)
            {
                return new()
                {
                    ProjectUId = Guid.NewGuid(),
                    Name = name,
                    Subtitle = subtitle,
                    Description = description,
                    GitHubURL = gitHubURL,
                    IsDeleted = false,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ProjectImages = images
                };
            }
        }
    }
}
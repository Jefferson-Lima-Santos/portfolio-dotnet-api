namespace Portfolio.Api.Domain.Entities
{
    public class ProjectImages
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string ImagePath { get; set; }
        public virtual Project Project { get; set; }
    }
}

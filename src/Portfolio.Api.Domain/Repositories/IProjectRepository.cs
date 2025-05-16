using Portfolio.Api.Domain.Entities;

namespace Portfolio.Api.Domain.Repositories
{
    public interface IProjectRepository
    {
        void Create(Project project, CancellationToken cancellationToken);
        Task<Project> CreateAsync(Project project, CancellationToken cancellationToken);
    }
}

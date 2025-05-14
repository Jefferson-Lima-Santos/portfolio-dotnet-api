using Portfolio.Api.Domain.Extensions;
using Portfolio.Api.Domain.Projects.DTOs;

namespace Portfolio.Api.Domain.Repositories
{
    public interface IProjectReadOnlyRepository
    {
        Task<PagedList<ProjectDto>> GetAllProjectsPaginated(PageFilterModel pageFilterModel,
            CancellationToken cancellationToken);
    }
}

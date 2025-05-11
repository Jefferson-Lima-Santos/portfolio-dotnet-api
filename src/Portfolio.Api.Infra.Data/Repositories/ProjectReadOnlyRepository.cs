using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Api.Domain.Entities;
using Portfolio.Api.Domain.Extensions;
using Portfolio.Api.Domain.Projects.DTOs;
using Portfolio.Api.Domain.Repositories;
using Portfolio.Api.Infra.Data.Context;
using Portfolio.Api.Infra.Data.Data;

namespace Portfolio.Api.Infra.Data.Repositories
{
    public class ProjectReadOnlyRepository : GenericReadOnlyRepository<Project>, IProjectReadOnlyRepository
    {
        public ProjectReadOnlyRepository(PortfolioDbContext portfolioDbContext,
            ILogger<ProjectReadOnlyRepository> logger) : base(portfolioDbContext, logger)
        {
        }

        public async Task<PagedList<ProjectDto>> GetAllProjectsPaginated(PageFilterModel pageFilterModel,
            CancellationToken cancellationToken)
        {
            var rep2 = await Entities.ToListAsync();
            var rep3 = await Entities.AsNoTracking().FirstOrDefaultAsync();
            var resp4 = await _portfolioDbContext.Projects.AsNoTracking().FirstOrDefaultAsync();
            var resp3 = await _portfolioDbContext.Projects.AsNoTracking().ToListAsync();
            var resp = await (from l in Entities.AsNoTracking()
                        join s in _portfolioDbContext.Projects.AsNoTracking()
                            on l.Id equals s.Id
                        join k in _portfolioDbContext.ProjectImages.AsNoTracking()
                            on l.Id equals k.ProjectId into projectImagesGroup
                        from nu in projectImagesGroup.DefaultIfEmpty()
                        group new { l, nu } by new
                        {
                            l.Id,
                            l.Name,
                            l.Subtitle,
                            l.Description,
                            l.GitHubURL
                        } into g
                        select new ProjectDto
                        {
                            Id = g.Key.Id,
                            Name = g.Key.Name,
                            Subtitle = g.Key.Subtitle,
                            Description = g.Key.Description,
                            GitHubURL = g.Key.GitHubURL,
                            Images = g.Where(x => x.nu != null).Select(x => x.nu.ImagePath).Distinct().ToList()
                        })
                    .PagingAsync(pageFilterModel.PageSize, pageFilterModel.page, cancellationToken);
            return resp;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Api.Domain.Entities;
using Portfolio.Api.Domain.Repositories;
using Portfolio.Api.Infra.Data.Context;
using Portfolio.Api.Infra.Data.Data;
using System.Collections.Generic;

namespace Portfolio.Api.Infra.Data.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly PortfolioDbContext _portfolioDbContext;
        public ProjectRepository(PortfolioDbContext portfolioDbContext,
            ILogger<ProjectRepository> logger) : base(portfolioDbContext, logger)
        {
            _portfolioDbContext = portfolioDbContext;
        }

        public void Create(Project project, CancellationToken cancellationToken)
        {
            _portfolioDbContext.Add(project);
        }
        public async Task<Project> CreateAsync(Project project, CancellationToken cancellationToken)
        {
            await _portfolioDbContext.AddAsync(project, cancellationToken);

            return project;
        }
    }
}

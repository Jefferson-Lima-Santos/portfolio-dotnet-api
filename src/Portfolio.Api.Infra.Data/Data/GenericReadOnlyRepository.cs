using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Api.Infra.Data.Context;

namespace Portfolio.Api.Infra.Data.Data
{
    public class GenericReadOnlyRepository<TEntity> where TEntity : class
    {
           protected readonly PortfolioDbContext _portfolioDbContext;
           private DbSet<TEntity> _dbSet;
           protected readonly ILogger _logger;
           public IQueryable<TEntity> Entities => _dbSet;

           public GenericReadOnlyRepository(PortfolioDbContext portfolioDbContext, ILogger logger)
           {
               _portfolioDbContext = portfolioDbContext;
               _dbSet = _portfolioDbContext.Set<TEntity>();
               _logger = logger;
           }
    }
}

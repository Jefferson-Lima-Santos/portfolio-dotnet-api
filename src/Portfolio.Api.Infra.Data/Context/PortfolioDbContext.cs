using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Portfolio.Api.Infra.Data.Context
{
    public class PortfolioDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PortfolioDbContext(
            DbContextOptions<PortfolioDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("ptf");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);

                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), sql =>
                    {
                        sql.MigrationsHistoryTable("__EFMigrationsHistory", "ptf");
                        sql.EnableRetryOnFailure();
                    });
            }
        }
    }
}

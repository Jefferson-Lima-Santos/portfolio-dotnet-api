using Microsoft.EntityFrameworkCore.Storage;
using Portfolio.Api.Domain.Repositories;


namespace Portfolio.Api.Domain.Data
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }
        IDbContextTransaction Begin();
        bool Commit();
        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
        void RejectChanges();
        Task<bool> ExecuteStrategyAsync(IEnumerable<Func<Task>> actions, CancellationToken cancellationToken);
    }
}

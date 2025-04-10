using Microsoft.EntityFrameworkCore.Storage;


namespace Portfolio.Api.Domain.Data
{
    public interface IUnitOfWork
    {
        IDbContextTransaction Begin();
        bool Commit();
        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
        void RejectChanges();
        Task<bool> ExecuteStrategyAsync(IEnumerable<Func<Task>> actions, CancellationToken cancellationToken);
    }
}

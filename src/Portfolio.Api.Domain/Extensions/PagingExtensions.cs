using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api.Domain.Extensions
{
    public static class PagingExtensions
    {
        public static PagedList<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 15,
            int pageNumber = 1) where TModel : class
        {
            var count = query.Count();
            var items = pageSize > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList() : query.ToList();
            return new PagedList<TModel>(items, count, pageNumber, pageSize);
        }

        public async static Task<PagedList<TModel>> PagingAsync<TModel>(this IQueryable<TModel> query,
            int pageSize = 15, int pageNumber = 1, CancellationToken cancellationToken = default) where TModel : class
        {
            var count = await query.CountAsync(cancellationToken);
            var items = pageSize > 0 && pageNumber > 0
                ? await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken)
                : await query.ToListAsync(cancellationToken);
            return new PagedList<TModel>(items, count, pageNumber, pageSize);
        }
    }

    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int page, int pageSize)
        {
            TotalCount = count;
            CurrentPage = page;
            PageSize = pageSize != 0 ? pageSize : count;
            TotalPages = (int)Math.Ceiling((double)count / (pageSize != 0 ? pageSize : count));
            AddRange(items);
        }
    }

    public class PageFilterModel
    {
        public int page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}

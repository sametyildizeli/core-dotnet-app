using Microsoft.EntityFrameworkCore;

namespace core_dotnet.Utilities.Paging
{
    public class PageList<T> : List<T> where T : class
    {
        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPage => PageSize == 0 ? 1 : (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPage;

        public PageList(List<T> currentPageItems, int pageItemCount, int currentPageNumber, int currentPageSize)
        {
            TotalCount = pageItemCount;
            PageSize = currentPageSize;
            CurrentPage = currentPageNumber;
            AddRange(currentPageItems);
        }

        public static async Task<PageList<T>> ToPageList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var pageItems = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PageList<T>(pageItems, count, pageNumber, pageSize);
        }

        public static PageList<T> ToListToPageList(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count;
            var pageItems = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PageList<T>(pageItems, count, pageNumber, pageSize);
        }
    }
}

namespace core_dotnet.Utilities.Paging;

public class PagedResult<TDto>
{
    public IEnumerable<TDto>? Results { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
namespace core_dotnet.Utilities.Paging
{
    public class PaginationEntity
    {
        public PaginationEntity()
        {
            PageNumber = 1;
        }

        const int maxPageItemSize = 10;

        private int _pageSize = 1;

        public int PageNumber { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageItemSize) ? maxPageItemSize : value;
            }
        }
    }
}

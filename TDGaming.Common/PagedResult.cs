namespace TDGaming.API.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalNumber { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

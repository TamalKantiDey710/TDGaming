using System.Collections.Generic;

namespace TDGaming.API.Common
{
    public class PagedResult1<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalNumber { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

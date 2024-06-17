using System.Collections.Generic;

namespace TS.Common.Models.Pagination
{
    /// <summary>
    /// PaginatedResponse model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResponse<T>
    {
        public IList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public long TotalCount { get; set; }
    }
}
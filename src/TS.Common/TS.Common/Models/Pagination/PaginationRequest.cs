using System.Collections.Generic;
using MongoDB.Driver;

namespace TS.Common.Models.Pagination
{
    /// <summary>
    /// Represents generic pagination request
    /// </summary>
    public class PaginationRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public SortDirection? SortDirection { get; set; }

        public string? SortField { get; set; }

        public List<PaginationFilterDto>? Filters { get; set; }
    }
}
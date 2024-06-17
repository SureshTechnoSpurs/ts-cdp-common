namespace TS.Common.Models.Pagination
{
    /// <summary>
    /// Represents search mode for the pagination filters
    /// </summary>
    public enum SearchMode
    {
        Contains = 1,
        LessThen = 2,
        LessOrEqualsThen = 3,
        GreaterThen = 4,
        GreaterOrEqualsThen = 5,
        Equals = 6,
        NotEquals = 7
    }
}
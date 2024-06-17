namespace TS.Common.Models.Pagination
{
    /// <summary>
    /// Represents pagination filter
    /// </summary>
    public class PaginationFilterDto
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public SearchMode SearchMode { get; set; }

        public FieldType FieldType { get; set; }
    }
}

namespace TS.Common.Entities
{
    /// <summary>
    /// Customer informatin
    /// </summary>
    public interface ICustomerEntity : IEntity
    {
        public string Name { get; set; }
        public string POSType { get; set; }
        public bool IsHistoricalDataIngested { get; set; }
        public int HistoricalIngestDaysPeriod { get; set; }
    }
}

using System;

namespace TS.Common.Entities
{
    /// <summary>
    /// Transaction information
    /// </summary>
    public interface ITransactionEntity : IEntity
    {
        public string CustomerName { get; set; }
        public bool IsTransactionCompleted { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}